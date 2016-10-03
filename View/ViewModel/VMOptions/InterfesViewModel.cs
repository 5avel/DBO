using System;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using DBO.Model.DAL;
using DBO.ViewModel.MVVMLib;

namespace DBO.ViewModel
{
    public class InterfesViewModel : ViewModelBase
    {
        public InterfesViewModel() // КОНСТРУКТОР
        {
            Swatches = new SwatchesProvider().Swatches;
        }

      

        public ICommand ToggleBaseCommand { get; } = new RelayCommand(o => ApplyBase((bool)o));

        public IEnumerable<Swatch> Swatches { get; }

        public ICommand ApplyPrimaryCommand { get; } = new RelayCommand(o => ApplyPrimary((Swatch)o));

        public ICommand ApplyAccentCommand { get; } = new RelayCommand(o => ApplyAccent((Swatch)o));



        private static void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        private static void ApplyPrimary(Swatch swatch)
        {
            new PaletteHelper().ReplacePrimaryColor(swatch);
        }

        private static void ApplyAccent(Swatch swatch)
        {
            new PaletteHelper().ReplaceAccentColor(swatch);
        }
    }
}
