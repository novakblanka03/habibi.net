using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetelvizz.Helpers
{
    public static class SubjectNames
    {
        private static readonly Dictionary<string, string> _subjectNames = new()
        {
            ["ea_limba_si_literatura_romana"] = "Román nyelv és irodalom",
            ["eb_limba_si_literatura_materna"] = "Anyanyelv és irodalom",
            ["ec_matematica"] = "Matematika",
            ["ec_istorie"] = "Történelem",
            ["ed_anatomie_biologie"] = "Anatómia és biológia",
            ["ed_chimie"] = "Kémia",
            ["ed_fizika"] = "Fizika",
            ["ed_geografie"] = "Földrajz",
            ["ed_informatica"] = "Informatika",
            ["ed_socioumane"] = "Társadalomtudományok",
            ["limba_si_literatura_romana"] = "Román nyelv és irodalom",
            ["matematica"] = "Matematika",
            ["limba_si_literatura_materna"] = "Anyanyelv és irodalom"
        };

        public static string GetName(string key) =>
            _subjectNames.TryGetValue(key, out var name) ? name : "Ismeretlen tantárgy";
    }
}

