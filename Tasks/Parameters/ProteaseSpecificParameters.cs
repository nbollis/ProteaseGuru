using Nett;
using Omics.Digestion;
using Omics.Modifications;
using Proteomics.ProteolyticDigestion;

namespace Tasks;

[TreatAsInlineTable]
public class ProteaseSpecificParameters
{
    public ProteaseSpecificParameters()
    {
        DigestionParams = new DigestionParams();
        FixedMods = new();
        VariableMods = new();
    }

    public ProteaseSpecificParameters(IDigestionParams digestionParams,
        List<Modification>? fixedMods = null,
        List<Modification>? variableMods = null)
    {
        DigestionParams = digestionParams;
        FixedMods = fixedMods ?? new List<Modification>();
        VariableMods = variableMods ?? new List<Modification>();
    }

    public string DigestionAgentName => DigestionParams.DigestionAgent.Name;
    public IDigestionParams DigestionParams { get; set; }
    public List<Modification> FixedMods { get; set; }
    public List<Modification> VariableMods { get; set; }
}
