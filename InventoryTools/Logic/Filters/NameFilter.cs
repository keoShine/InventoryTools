using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using InventoryTools.Extensions;
using InventoryTools.Logic.Filters.Abstract;
using Lumina.Excel.GeneratedSheets;
using System;
using System.Text.RegularExpressions;
using Dalamud.Logging;

namespace InventoryTools.Logic.Filters
{
    public class NameFilter : StringFilter
    {
        public override string Key { get; set; } = "Name";
        public override string Name { get; set; } = "Name";
        public override string HelpText { get; set; } = "Searches by the name of the item.";
        public override FilterType AvailableIn { get; set; }  = FilterType.SearchFilter | FilterType.SortingFilter | FilterType.GameItemFilter;
        public override FilterCategory FilterCategory { get; set; } = FilterCategory.Basic;

        private bool IsTextRegex(string x)
        { 
            return x.StartsWith('/') && x.EndsWith('/');
        }

        public override bool? FilterItem(FilterConfiguration configuration,InventoryItem item)
        {
            return FilterItem(configuration, item.Item);
        }

        public override bool? FilterItem(FilterConfiguration configuration, ItemEx item)
        {
            var currentValue = CurrentValue(configuration);
            if (!string.IsNullOrEmpty(currentValue))
            {
                try
                {
                    if (IsTextRegex(currentValue))
                    {
                        currentValue = currentValue.Trim('/');
                        if (!Regex.Match(item.NameString.ToString(), currentValue, RegexOptions.IgnoreCase).Success)
                        {
                            return false;
                        }
                    }
                    else
                    {

                        if (!item.NameString.ToString().ToLower().PassesFilter(currentValue.ToLower()))
                        {
                            return false;
                        }
                    }
                }
                catch (Exception e)
                {
                    PluginLog.Error(e, "Invalid Regex String");
                    return false;
                }
            }

            return true;
        }
    }
}