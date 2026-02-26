namespace Tasks;

//digestion parameters provided by the user
public class RunParameters : ParameterBaseClass<RunParameters>
{
    public string OutputFolder { get; set; } = string.Empty;
    public bool TreatModifiedPeptidesAsDifferent { get; set; } = false;
    public int MinPeptideMassAllowed { get; set; } = -1;
    public int MaxPeptideMassAllowed { get; set; } = -1;
    public List<ProteaseSpecificParameters> ProteaseSpecificParameters { get; set; } = new();

    public RunParameters Clone()
    {
        return new RunParameters
        {
            OutputFolder = this.OutputFolder,
            TreatModifiedPeptidesAsDifferent = this.TreatModifiedPeptidesAsDifferent,
            MinPeptideMassAllowed = this.MinPeptideMassAllowed,
            MaxPeptideMassAllowed = this.MaxPeptideMassAllowed,
            ProteaseSpecificParameters = this.ProteaseSpecificParameters.Select(p => p.Clone()).ToList()
        };
    }
}
