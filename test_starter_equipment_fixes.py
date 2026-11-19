import contextlib
import importlib.util
import io
import os
import sys
import unittest


SERVER_PATH = os.path.join(os.path.dirname(__file__), "9555.py")
SPEC = importlib.util.spec_from_file_location("game_server", SERVER_PATH)
SERVER = importlib.util.module_from_spec(SPEC)
sys.modules[SPEC.name] = SERVER
with contextlib.redirect_stdout(io.StringIO()):
    SPEC.loader.exec_module(SERVER)


class StarterEquipmentFixTests(unittest.TestCase):
    def test_equipment_inherit_tags_match_client_order(self):
        body = {0: 0, 1: 12001, 2: 1, 3: 22002}

        self.assertEqual(
            SERVER.parse_equip_inherit_request(body),
            (12001, 0, 22002, 1)
        )

    def test_updater_decodes_bundle_names_and_recovers_missing_version(self):
        relative, local_path = SERVER.resolve_resource_http_path(
            "/RES_-001/Bundle/Items/Texture/CZ_%244.99.bundle?cache=1"
        )

        self.assertEqual(
            relative, "RES_205/Bundle/Items/Texture/CZ_$4.99.bundle"
        )
        self.assertTrue(os.path.isfile(local_path))

    def test_updater_path_resolution_rejects_traversal(self):
        _relative, local_path = SERVER.resolve_resource_http_path(
            "/RES_-001/../../9555.py"
        )

        self.assertIsNone(local_path)

    def test_legacy_jjc_skills_do_not_leak_into_player_skill_maps(self):
        for profession, legacy_skill in enumerate(("105", "205", "305")):
            with self.subTest(profession=profession):
                levels = {legacy_skill: 7, "106": 2}
                skills = SERVER.build_skills_map(
                    profession, 20, levels, equipped_items=[]
                )
                starter_active = SERVER.PROF_SKILLS[profession]["starter_active"]

                self.assertIn(starter_active, skills)
                self.assertIn("106", skills)
                self.assertNotIn(legacy_skill, skills)
                self.assertNotIn(
                    legacy_skill, SERVER.learned_non_weapon_skill_ids(levels)
                )

                character = {
                    "prof": profession,
                    "level": 20,
                    "skill_levels": {legacy_skill: 7},
                }
                result = SERVER.apply_skill_level_up(
                    character, legacy_skill, client_level=7
                )
                self.assertEqual(result, (0, 7, False))
                self.assertEqual(character["skill_levels"], {legacy_skill: 7})


if __name__ == "__main__":
    unittest.main()
