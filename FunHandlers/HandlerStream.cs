using TSLab.Script.Handlers;
using TSLab.Script.Handlers.Options;

namespace MyLib
{
    [HandlerCategory("FunHandlers")]
    [HelperName("HandlerStream", Language = Constants.En)]
    [InputsCount(2)]
    [Input(0, TemplateTypes.DOUBLE, false, "list1")]
    [Input(1, TemplateTypes.DOUBLE, false, "list2")]
    [OutputsCount(1)]
    [OutputType(TemplateTypes.DOUBLE)]
    public class HandlerStream : IStreamHandler
    {
        public IList<double> Execute(IList<double> list1, IList<double> list2)
        {
            var result = new double[list1.Count];
            for (int i = 0; i < list1.Count; i++)
            {
                result[i] = (list1[i] + list2[i]) / 2;
            }
            return result;
        }
    }
}
