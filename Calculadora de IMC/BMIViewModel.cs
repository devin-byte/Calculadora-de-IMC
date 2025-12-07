using System;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CalculadoraIMCApp.Models;

namespace CalculadoraIMCApp.ViewModels
{
    public partial class BMIViewModel : ObservableObject
    {
        // Entrada como texto (más robusto con diferentes locales y permite validación)
        [ObservableProperty]
        private string pesoText;

        [ObservableProperty]
        private string alturaText;

        // Resultado y mensaje que se mostrarán en la vista
        [ObservableProperty]
        private string resultadoIMC;

        [ObservableProperty]
        private string rangoIMC;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool isResultVisible;

        public BMIViewModel()
        {
            PesoText = string.Empty;
            AlturaText = string.Empty;
            ResultadoIMC = string.Empty;
            RangoIMC = string.Empty;
            ErrorMessage = string.Empty;
            IsResultVisible = false;
        }

        // Comando para calcular el IMC
        [RelayCommand]
        private void CalcularIMC()
        {
            ErrorMessage = string.Empty;
            IsResultVisible = false;
            ResultadoIMC = string.Empty;
            RangoIMC = string.Empty;

            // Normalizar entrada: permitir coma o punto decimal
            string pesoNorm = (PesoText ?? string.Empty).Trim().Replace(',', '.');
            string alturaNorm = (AlturaText ?? string.Empty).Trim().Replace(',', '.');

            if (!double.TryParse(pesoNorm, NumberStyles.Number, CultureInfo.InvariantCulture, out double peso) ||
                !double.TryParse(alturaNorm, NumberStyles.Number, CultureInfo.InvariantCulture, out double alturaCm))
            {
                ErrorMessage = "Ingrese valores numéricos válidos para peso y altura.";
                return;
            }

            if (peso <= 0)
            {
                ErrorMessage = "El peso debe ser mayor que cero.";
                return;
            }

            if (alturaCm <= 0)
            {
                ErrorMessage = "La altura debe ser mayor que cero.";
                return;
            }

            // Construir modelo (no estrictamente necesario, pero sigue el patrón)
            var datos = new IMCData { PesoKg = peso, AlturaCm = alturaCm };

            double alturaM = datos.AlturaCm / 100.0;
            double imc = datos.PesoKg / (alturaM * alturaM);

            // Formato con 2 decimales
            ResultadoIMC = $"IMC: {imc:F2}";
            RangoIMC = ObtenerRango(imc);
            IsResultVisible = true;
        }

        // Comando para limpiar los campos
        [RelayCommand]
        private void Limpiar()
        {
            PesoText = string.Empty;
            AlturaText = string.Empty;
            ResultadoIMC = string.Empty;
            RangoIMC = string.Empty;
            ErrorMessage = string.Empty;
            IsResultVisible = false;
        }

        // Determina el rango según valores clásicos de IMC
        private string ObtenerRango(double imc)
        {
            // Clasificación general (OMS)
            // <18.5 Bajo peso
            // 18.5–24.9 Normal
            // 25.0–29.9 Sobrepeso
            // >=30 Obesidad
            if (imc < 18.5) return "Rango: Bajo peso (IMC < 18.5)";
            if (imc >= 18.5 && imc < 25.0) return "Rango: Normal (18.5 - 24.9)";
            if (imc >= 25.0 && imc < 30.0) return "Rango: Sobrepeso (25.0 - 29.9)";
            return "Rango: Obesidad (IMC ≥ 30.0)";
        }
    }
}
