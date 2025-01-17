using System.Collections.Generic;
using System.Numerics;
using CriticalCommonLib.Crafting;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using ImGuiNET;

namespace InventoryTools.Logic.Columns.Abstract
{
    public abstract class GameIconColumn : Column<(ushort,bool)?>
    {
        public override string CsvExport(InventoryItem item)
        {
            return "";
        }

        public override string CsvExport(ItemEx item)
        {
            return "";
        }

        public override string CsvExport(SortingResult item)
        {
            return "";
        }
        public override (ushort,bool)? CurrentValue(CraftItem currentValue)
        {
            return CurrentValue(currentValue.Item);
        }
        
        public override IEnumerable<CraftItem> Filter(IEnumerable<CraftItem> items)
        {
            return items;
        }

        public override IEnumerable<CraftItem> Sort(ImGuiSortDirection direction, IEnumerable<CraftItem> items)
        {
            return items;
        }
        public virtual Vector2 IconSize
        {
            get
            {
                return new Vector2(32, 32);
            }
        }
        public virtual string EmptyText
        {
            get
            {
                return "N/A";
            }
        }
        public override void Draw(FilterConfiguration configuration, InventoryItem item, int rowIndex)
        {
            var result = DoDraw(CurrentValue(item), rowIndex);
            result?.HandleEvent(configuration, item);
        }
        public override void Draw(FilterConfiguration configuration, SortingResult item, int rowIndex)
        {
            var result = DoDraw(CurrentValue(item), rowIndex);
            result?.HandleEvent(configuration, item);
        }
        public override void Draw(FilterConfiguration configuration, ItemEx item, int rowIndex)
        {
            var result = DoDraw(CurrentValue((ItemEx)item), rowIndex);
            result?.HandleEvent(configuration, item);
        }
        public override void Draw(FilterConfiguration configuration, CraftItem item, int rowIndex)
        {
            var result = DoDraw(CurrentValue(item), rowIndex);
            result?.HandleEvent(configuration, item);
        }

        public override IEnumerable<ItemEx> Filter(IEnumerable<ItemEx> items)
        {
            return items;
        }

        public override IEnumerable<InventoryItem> Filter(IEnumerable<InventoryItem> items)
        {
            return items;
        }

        public override IEnumerable<SortingResult> Filter(IEnumerable<SortingResult> items)
        {
            return items;
        }

        public override IEnumerable<InventoryItem> Sort(ImGuiSortDirection direction, IEnumerable<InventoryItem> items)
        {
            return items;
        }

        public override IEnumerable<ItemEx> Sort(ImGuiSortDirection direction, IEnumerable<ItemEx> items)
        {
            return items;
        }

        public override IEnumerable<SortingResult> Sort(ImGuiSortDirection direction, IEnumerable<SortingResult> items)
        {
            return items;
        }

        public override IColumnEvent? DoDraw((ushort,bool)? currentValue, int rowIndex)
        {
            ImGui.TableNextColumn();
            if (currentValue != null)
            {
                PluginService.PluginLogic.DrawIcon(currentValue.Value.Item1, IconSize, currentValue.Value.Item2);
            }
            return null;
        }

        public override void Setup(int columnIndex)
        {
            ImGui.TableSetupColumn(Name, ImGuiTableColumnFlags.WidthFixed, Width, (uint)columnIndex);
        }
    }
}