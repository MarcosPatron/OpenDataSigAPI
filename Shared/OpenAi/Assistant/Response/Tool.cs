
namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class Tool
    {
        public string Type { get; set; }
    }

    public class CodeInterpreterTool : Tool
    {
        public CodeInterpreterTool()
        {
            Type = "code_interpreter";
        }
    }

    public class RetrievalTool : Tool 
    {
        public RetrievalTool()
        {
            Type = "retrieval";
        }
    }

    public class FunctionTool : Tool
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public object Parameters { get; set; } // Consider using a more specific type or structure to represent JSON Schema

        public FunctionTool()
        {
            Type = "function";
        }
    }
}
