using System.Collections.Generic;
using System.Numerics;
using CriticalCommonLib.Enums;
using CriticalCommonLib.Services.Ui;
using InventoryTools.Logic;

namespace InventoryTools.GameUi
{
    public class InventoryRetainerOverlay : AtkInventoryRetainer, IAtkOverlayState
    {
        public override bool Draw()
        {
            if (!HasState || !HasAddon)
            {
                return false;
            }
            var atkUnitBase = AtkUnitBase;
            if (atkUnitBase != null)
            {
                this.SetTabColors(TabColours);
                if (CurrentTab == 0)
                {
                    this.SetColors(InventoryType.RetainerBag0, Bag1InventoryColours);
                }
                else if (CurrentTab == 1)
                {
                    this.SetColors(InventoryType.RetainerBag1, Bag2InventoryColours);
                }
                else if (CurrentTab == 2)
                {
                    this.SetColors(InventoryType.RetainerBag2, Bag3InventoryColours);
                }
                else if (CurrentTab == 3)
                {
                    this.SetColors(InventoryType.RetainerBag3, Bag4InventoryColours);
                }
                else if (CurrentTab == 4)
                {
                    this.SetColors(InventoryType.RetainerBag4, Bag5InventoryColours);
                }
                else if (CurrentTab == 5)
                {
                    this.SetColors(InventoryType.RetainerBag0, Bag1InventoryColours);
                }

                return true;
            }

            return false;
        }
        
        public Dictionary<Vector2, Vector4?> Bag1InventoryColours = new();
        public Dictionary<Vector2, Vector4?> Bag2InventoryColours = new();
        public Dictionary<Vector2, Vector4?> Bag3InventoryColours = new();
        public Dictionary<Vector2, Vector4?> Bag4InventoryColours = new();
        public Dictionary<Vector2, Vector4?> Bag5InventoryColours = new();
        public Dictionary<uint, Vector4?> TabColours = new();
        public Dictionary<Vector2, Vector4?> EmptyDictionary = new();
        public Dictionary<uint, Vector4?> EmptyTabs = new() { {0, null}, {1, null}, {2, null}, {3, null} };

        public override void Setup()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    EmptyDictionary.Add(new Vector2(x,y), null);
                }
            }

        }

        public bool HasState { get; set; }
        public bool NeedsStateRefresh { get; set; }

        public void UpdateState(FilterState? newState)
        {
            if (PluginService.CharacterMonitor.ActiveCharacter == 0)
            {
                return;
            }
            if (newState != null && HasAddon && newState.Value.ShouldHighlight && newState.Value.HasFilterResult)
            {
                HasState = true;
                var filterResult = newState.Value.FilterResult;
                if (filterResult.HasValue)
                {
                    Bag1InventoryColours = newState.Value.GetBagHighlights(InventoryType.RetainerBag0);
                    Bag2InventoryColours = newState.Value.GetBagHighlights(InventoryType.RetainerBag1);
                    Bag3InventoryColours = newState.Value.GetBagHighlights(InventoryType.RetainerBag2);
                    Bag4InventoryColours = newState.Value.GetBagHighlights(InventoryType.RetainerBag3);
                    Bag5InventoryColours = newState.Value.GetBagHighlights(InventoryType.RetainerBag4);
                    var tab1 = newState.Value.GetTabHighlight(Bag1InventoryColours);
                    var tab2 = newState.Value.GetTabHighlight(Bag2InventoryColours);
                    var tab3 = newState.Value.GetTabHighlight(Bag3InventoryColours);
                    var tab4 = newState.Value.GetTabHighlight(Bag4InventoryColours);
                    var tab5 = newState.Value.GetTabHighlight(Bag5InventoryColours);
                    TabColours[0] = tab1;
                    TabColours[1] = tab2;
                    TabColours[2] = tab3;
                    TabColours[3] = tab4;
                    TabColours[4] = tab5;
                    Draw();
                    return;
                }
            }
            
            if (HasState)
            {
                Bag1InventoryColours = EmptyDictionary;
                Bag2InventoryColours = EmptyDictionary;
                Bag3InventoryColours = EmptyDictionary;
                Bag4InventoryColours = EmptyDictionary;
                Bag5InventoryColours = EmptyDictionary;
                Clear();
            }

            HasState = false;
        }

        public void Clear()
        {
            var atkUnitBase = AtkUnitBase;
            if (atkUnitBase != null)
            {
                this.SetColors(InventoryType.RetainerBag0, EmptyDictionary);
                this.SetTabColors(EmptyTabs);
            }
        }
    }
}