using Nett;
using Omics.Digestion;
using Omics.Modifications;
using Proteomics.ProteolyticDigestion;
using Transcriptomics.Digestion;

namespace Tasks;

public abstract class ParameterBaseClass<TParameters> where TParameters : ParameterBaseClass<TParameters>
{
    public static void ToToml(TParameters parameters, string filePath)
    {
        Toml.WriteFile(parameters, filePath);
    }

    public static TParameters FromToml(TParameters parameters, string filePath) 
    {
        TParameters parametersFromFile = Toml.ReadFile<TParameters>(filePath);
        foreach (var property in typeof(TParameters).GetProperties())
        {
            var value = property.GetValue(parametersFromFile);
            property.SetValue(parameters, value);
        }
        return parameters;
    }

    public static readonly TomlSettings TomlConfig = TomlSettings.Create(cfg => cfg
        .ConfigureType<IDigestionParams>(type => type
            .WithConversionFor<TomlTable>(c => c
                .FromToml(tmlTable =>
                    tmlTable.ContainsKey("Protease")
                        ? tmlTable.Get<DigestionParams>()
                        : tmlTable.Get<RnaDigestionParams>())))
        .ConfigureType<DigestionParams>(type => type
            .IgnoreProperty(p => p.DigestionAgent)
            .IgnoreProperty(p => p.MaxMods)
            .IgnoreProperty(p => p.MaxLength)
            .IgnoreProperty(p => p.MinLength))
        .ConfigureType<RnaDigestionParams>(type => type
            .IgnoreProperty(p => p.DigestionAgent))
        .ConfigureType<Rnase>(type => type
            .WithConversionFor<TomlString>(convert => convert
                .ToToml(custom => custom.Name)
                .FromToml(tmlString => RnaseDictionary.Dictionary[tmlString.Value])))
        .ConfigureType<Protease>(type => type
            .WithConversionFor<TomlString>(convert => convert
                .ToToml(custom => custom.ToString())
                .FromToml(tmlString => ProteaseDictionary.Dictionary[tmlString.Value])))
        //.ConfigureType<Modification>(type => type
        //    .WithConversionFor<TomlString>(convert => convert
        //        .ToToml(custom => custom.ToString())
        //        .FromToml(tmlString => ModificationDictionary.Dictionary[tmlString.Value])))
        );
}
