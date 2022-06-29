using System.ComponentModel;
using TSLab.Script.Handlers;
using TSLab.Script.Handlers.Options;

namespace FunHandlers
{
    [HandlerCategory("FunHandlers")]
    [HelperName("Pearson Linear Correlation Coefficient", Language = Constants.En)]
    [HelperName("Линейный коэффициент корреляции Пирсона", Language = Constants.Ru)]
    [InputsCount(2)]
    [Input(0, TemplateTypes.DOUBLE, false, "x")]
    [Input(1, TemplateTypes.DOUBLE, false, "y")]
    [OutputsCount(1)]
    [OutputType(TemplateTypes.DOUBLE)]
    [Description("Рассчитывает линейный коэффициент корреляции Пирсона")]
    [HelperDescription("Calculates the linear Pearson correlation coefficient", Constants.En)]
    public class LKKP : IStreamHandler
    {
        [Description("Период индикатора")]
        [HandlerParameter(true, "20", Min = "10", Max = "100", Step = "5", EditorMin = "1")]
        [HelperDescription("Indicator period", Constants.En)]
        [HelperName("Period", Constants.En)]
        [HelperName("Период", Constants.Ru)]
        public int Period { get; set; }

        public IList<double> Execute(IList<double> x, IList<double> y)
        {
            var result = new double[x.Count];
            for (int i = Period; i < x.Count; i++)
            {
                double Xaver = 0.0, Yaver = 0.0, sum=0.0, sumX = 0.0, sumY = 0.0;

                for (int j = i; j > i - Period; j--)
                {
                    Xaver += x[j];
                    Yaver += y[j];
                }
                Xaver /= Period;
                Yaver /= Period;

                for (int k = i; k > i - Period; k--)
                {
                    sum += (x[k] - Xaver) * (y[k] - Yaver);
                    sumX += (x[k] - Xaver) * (x[k] - Xaver);
                    sumY += (y[k] - Yaver) * (y[k] - Yaver);
                }

                result[i] = sum / Math.Sqrt(sumX * sumY);
            }

            return result;
        }
    }
}
