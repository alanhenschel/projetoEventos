using System;
using System.Text.Json;

namespace ProEventos.API
{
    public class SnakeCasePropertyNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            var sb = new System.Text.StringBuilder();

            for (int i = 0; i < name.Length; i++)
            {
                if (i > 0 && char.IsUpper(name[i])) {
                    sb.Append("_" + name[i]);
                } else {
                    sb.Append(name[i].ToString());
                }
            }
            return sb.ToString().ToLower();
        }
    }
}