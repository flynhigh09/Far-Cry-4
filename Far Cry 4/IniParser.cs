using System;
using System.Collections;
using System.IO;

public class IniParser
{
  private Hashtable keyPairs = new Hashtable();
  private string iniFilePath;

  public IniParser(string iniPath)
  {
    TextReader textReader = (TextReader) null;
    string str1 = (string) null;
    this.iniFilePath = iniPath;
    if (!File.Exists(iniPath))
      throw new FileNotFoundException("Unable to locate " + iniPath);
    try
    {
      textReader = (TextReader) new StreamReader(iniPath);
      for (string str2 = textReader.ReadLine(); str2 != null; str2 = textReader.ReadLine())
      {
        string str3 = str2.Trim().ToUpper();
        if (str3 != "")
        {
          if (str3.StartsWith("[") && str3.EndsWith("]"))
          {
            str1 = str3.Substring(1, str3.Length - 2);
          }
          else
          {
            string[] strArray = str3.Split(new char[1]
            {
              '='
            }, 2);
            string str4 = (string) null;
            if (str1 == null)
              str1 = "ROOT";
            IniParser.SectionPair sectionPair;
            sectionPair.Section = str1;
            sectionPair.Key = strArray[0];
            if (strArray.Length > 1)
              str4 = strArray[1];
            keyPairs.Add((object) sectionPair, (object) str4);
          }
        }
      }
    }
    catch (Exception ex)
    {
      throw ex;
    }
    finally
    {
      if (textReader != null)
        textReader.Close();
    }
  }

  public string GetSetting(string sectionName, string settingName)
  {
    IniParser.SectionPair sectionPair;
    sectionPair.Section = sectionName.ToUpper();
    sectionPair.Key = settingName.ToUpper();
    return (string) keyPairs[(object) sectionPair];
  }

  public string[] EnumSection(string sectionName)
  {
    ArrayList arrayList = new ArrayList();
    foreach (IniParser.SectionPair sectionPair in (IEnumerable) keyPairs.Keys)
    {
      if (sectionPair.Section == sectionName.ToUpper())
        arrayList.Add((object) sectionPair.Key);
    }
    return (string[]) arrayList.ToArray(typeof (string));
  }

  public void AddSetting(string sectionName, string settingName, string settingValue)
  {
    IniParser.SectionPair sectionPair;
    sectionPair.Section = sectionName.ToUpper();
    sectionPair.Key = settingName.ToUpper();
    if (keyPairs.ContainsKey((object) sectionPair))
      keyPairs.Remove((object) sectionPair);
    keyPairs.Add((object) sectionPair, (object) settingValue);
  }

  public void AddSetting(string sectionName, string settingName)
  {
    AddSetting(sectionName, settingName, (string) null);
  }

  public void DeleteSetting(string sectionName, string settingName)
  {
    IniParser.SectionPair sectionPair;
    sectionPair.Section = sectionName.ToUpper();
    sectionPair.Key = settingName.ToUpper();
    if (!keyPairs.ContainsKey((object) sectionPair))
      return;
    keyPairs.Remove((object) sectionPair);
  }

  public void SaveSettings(string newFilePath)
  {
    ArrayList arrayList = new ArrayList();
    string str1 = "";
    foreach (IniParser.SectionPair sectionPair in (IEnumerable) keyPairs.Keys)
    {
      if (!arrayList.Contains((object) sectionPair.Section))
        arrayList.Add((object) sectionPair.Section);
    }
    foreach (string str2 in arrayList)
    {
      str1 = str1 + "[" + str2 + "]\r\n";
      foreach (IniParser.SectionPair sectionPair in (IEnumerable) keyPairs.Keys)
      {
        if (sectionPair.Section == str2)
        {
          string str3 = (string) this.keyPairs[(object) sectionPair];
          if (str3 != null)
            str3 = "=" + str3;
          str1 = str1 + sectionPair.Key + str3 + "\r\n";
        }
      }
      str1 = str1 + "\r\n";
    }
    try
    {
      TextWriter textWriter = (TextWriter) new StreamWriter(newFilePath);
      textWriter.Write(str1);
      textWriter.Close();
    }
    catch (Exception ex)
    {
      throw ex;
    }
  }

  public void SaveSettings()
  {
    SaveSettings(iniFilePath);
  }

  private struct SectionPair
  {
    public string Section;
    public string Key;
  }
}
