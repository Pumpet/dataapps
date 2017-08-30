using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  /// <summary>транслитерация русского в латиницу 
  /// </summary>
  public static class Translit
  {
    private static Dictionary<string, string> dic = new Dictionary<string, string>(); // словарь соответствия
    static Translit()
    {
      dic.Add("а", "a");
      dic.Add("б", "b");
      dic.Add("в", "v");
      dic.Add("г", "g");
      dic.Add("д", "d");
      dic.Add("е", "e");
      dic.Add("ё", "yo");
      dic.Add("ж", "zh");
      dic.Add("з", "z");
      dic.Add("и", "i");
      dic.Add("й", "j");
      dic.Add("к", "k");
      dic.Add("л", "l");
      dic.Add("м", "m");
      dic.Add("н", "n");
      dic.Add("о", "o");
      dic.Add("п", "p");
      dic.Add("р", "r");
      dic.Add("с", "s");
      dic.Add("т", "t");
      dic.Add("у", "u");
      dic.Add("ф", "f");
      dic.Add("х", "h");
      dic.Add("ц", "c");
      dic.Add("ч", "ch");
      dic.Add("ш", "sh");
      dic.Add("щ", "sch");
      dic.Add("ъ", "j");
      dic.Add("ы", "i");
      dic.Add("ь", "j");
      dic.Add("э", "e");
      dic.Add("ю", "yu");
      dic.Add("я", "ya");
      dic.Add("А", "A");
      dic.Add("Б", "B");
      dic.Add("В", "V");
      dic.Add("Г", "G");
      dic.Add("Д", "D");
      dic.Add("Е", "E");
      dic.Add("Ё", "Yo");
      dic.Add("Ж", "Zh");
      dic.Add("З", "Z");
      dic.Add("И", "I");
      dic.Add("Й", "J");
      dic.Add("К", "K");
      dic.Add("Л", "L");
      dic.Add("М", "M");
      dic.Add("Н", "N");
      dic.Add("О", "O");
      dic.Add("П", "P");
      dic.Add("Р", "R");
      dic.Add("С", "S");
      dic.Add("Т", "T");
      dic.Add("У", "U");
      dic.Add("Ф", "F");
      dic.Add("Х", "H");
      dic.Add("Ц", "C");
      dic.Add("Ч", "Ch");
      dic.Add("Ш", "Sh");
      dic.Add("Щ", "Sch");
      dic.Add("Ъ", "J");
      dic.Add("Ы", "I");
      dic.Add("Ь", "J");
      dic.Add("Э", "E");
      dic.Add("Ю", "Yu");
      dic.Add("Я", "Ya");
    }

    /// <summary>Записать латиницей
    /// </summary>
    /// <param name="src">исходный текст</param>
    public static string GetTranslit(string src)
    {
      StringBuilder res = new StringBuilder();
      for (int i = 0; i < src.Length; i++)
        if (dic.ContainsKey(src[i].ToString()))
          res.Append(dic[src[i].ToString()]);
        else
          res.Append(src[i].ToString());
      return res.ToString();
    }
  }
}
