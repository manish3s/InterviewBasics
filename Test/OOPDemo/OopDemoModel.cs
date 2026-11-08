namespace Test.OOPDemo
{
    // View model used to pass explanations and results to the view
    public class OopDemoModel
    {
        // Initialize properties to empty strings to satisfy non-nullable reference checks
        public string EncapsulationDefinition { get; set; } = string.Empty;
        public string EncapsulationResult { get; set; } = string.Empty;

        public string InheritanceDefinition { get; set; } = string.Empty;
        public string InheritanceResult { get; set; } = string.Empty;

        public string PolymorphismDefinition { get; set; } = string.Empty;
        public string PolymorphismResult { get; set; } = string.Empty;

        public string AbstractionDefinition { get; set; } = string.Empty;
        public string AbstractionResult { get; set; } = string.Empty;

        public string InterfaceDefinition { get; set; } = string.Empty;
        public string InterfaceResult { get; set; } = string.Empty;
    }
}
