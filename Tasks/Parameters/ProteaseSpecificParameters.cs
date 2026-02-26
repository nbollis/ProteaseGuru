using Omics.Digestion;
using Omics.Modifications;

namespace Tasks;

public class ProteaseSpecificParameters(
    IDigestionParams digestionParams,
    List<Modification>? fixedMods = null,
    List<Modification>? variableMods = null)
{
    public string DigestionAgentName => DigestionParams.DigestionAgent.Name;
    public IDigestionParams DigestionParams { get; set; } = digestionParams;
    public List<Modification> FixedMods { get; set; } = fixedMods ?? new();
    public List<Modification> VariableMods { get; set; } = variableMods ?? new();
}
