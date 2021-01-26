using DiShare.Domain.Models;
using DiShare.Infrastructure;
using DiShare.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace DiShare.Logic.ScriptGenerators
{
  public class ScriptGenerator : IScriptGenerator
  {
    private static readonly IReadOnlyDictionary<ItemType, string> ScriptNames = (IReadOnlyDictionary<ItemType, string>) new Dictionary<ItemType, string>()
    {
      {
        ItemType.Model,
        "Model.ms"
      },
      {
        ItemType.Material,
        "MaxMaterial.ms"
      }
    };
    private readonly string tempDirectory;
    private readonly string templatesDirectory;

    public ScriptGenerator()
    {
      this.tempDirectory = Path.GetTempPath();
      this.templatesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
    }

    public TryResult<string> Generate(ModelItem item)
    {
      try
      {
        string path1 = Path.Combine(this.templatesDirectory, ScriptGenerator.ScriptNames[item.Type]);
        if (!File.Exists(path1))
          return new TryResult<string>((Exception) new ScriptGeneratorException("Template file not found: " + path1));
        string contents = File.ReadAllText(path1);
        if (item.Type == ItemType.Model)
          contents = contents.Replace("{ModelPath}", Path.Combine(item.Path, "Model") + "\\");
        else if (item.Type == ItemType.Material)
          contents = contents.Replace("{ModelPath}", Path.Combine(item.Path, "MaxMaterial") + "\\");
        string path2 = Path.Combine(/*this.tempDirectory*/item.Path, "DiScript_" + Guid.NewGuid().ToString() + ".ms");
        File.WriteAllText(path2, contents);
        return new TryResult<string>(path2);
      }
      catch (Exception ex)
      {
        return new TryResult<string>((Exception) new ScriptGeneratorException("Unhandled exception", ex));
      }
    }
  }
}
