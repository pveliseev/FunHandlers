using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Handlers.Options;
using TSLab.Script.Helpers;

namespace FunHandlers
{
    [HandlerCategory("FunHandlers")]
    [HelperName("Middle channel", Language = Constants.En)]
    [HelperName("Середина канала", Language = Constants.Ru)]
    [InputsCount(1)]
    [Input(0, TemplateTypes.SECURITY, Name = "SECURITYSource")]
    [OutputsCount(1)]
    [OutputType(TemplateTypes.DOUBLE)]
    [Description("Рассчитывает середину канала как (Highest + Lowest) / 2.")]
    [HelperDescription("Calculates the middle of the channel as (Highest + Lowest) / 2.", Constants.En)]
    public class MiddleChannel : IBar2DoubleHandler, IContextUses
    {
        public IContext Context { get; set; }

        [Description("Период индикатора")]
        [HandlerParameter(true, "20", Min = "10", Max = "100", Step = "5", EditorMin = "1")]
        [HelperDescription("Indicator period", Constants.En)]
        [HelperName("Period", Constants.En)]
        [HelperName("Период", Constants.Ru)]
        public int Period { get; set; }

        public IList<double> Execute(ISecurity source)
        {
            // Вычисляем максимумы и минимумы
            // Используем GetData для кеширования данных и ускорения оптимизации
            var high = Context.GetData("Highest", new[] { Period.ToString() },
                () => Series.Highest(source.GetHighPrices(Context), Period));
            var low = Context.GetData("Lowest", new[] { Period.ToString() },
                () => Series.Lowest(source.GetLowPrices(Context), Period));

            // Расчет значений
            var list = new double[source.Bars.Count];

            for (int i = 0; i < list.Length; i++)
            {
                list[i] = (high[i] + low[i]) / 2;
            }

            return list;
        }
    }
}
