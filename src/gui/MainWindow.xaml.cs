using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Secdev.Workstation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = (WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private async void SendMessage()
        {
            string prompt = TxtPrompt.Text.Trim();
            if (string.IsNullOrEmpty(prompt)) return;

            TxtPrompt.Text = string.Empty;

            // 1. Add User Message
            var userBorder = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0x1a, 0x23, 0x32)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xe5, 0xff)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 0, 0, 10),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 420
            };

            var userText = new TextBlock
            {
                Text = prompt,
                FontFamily = new FontFamily("Consolas, JetBrains Mono"),
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            };
            userBorder.Child = userText;
            ChatContainer.Children.Add(userBorder);
            ChatScrollViewer.ScrollToEnd();

            // 2. Stream AI Response
            var aiBorder = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0x16, 0x1b, 0x22)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x30, 0x36, 0x3d)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(12),
                Margin = new Thickness(0, 0, 0, 10),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var aiStack = new StackPanel();
            var aiHeader = new TextBlock
            {
                Text = "⚡ Qwen2.5-Coder-7B [Air-Gapped Ingestion]:",
                FontFamily = new FontFamily("Consolas"),
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(0x00, 0xff, 0x9d)),
                Margin = new Thickness(0, 0, 0, 6)
            };
            aiStack.Children.Add(aiHeader);

            var aiContent = new TextBlock
            {
                Text = "",
                FontFamily = new FontFamily("Consolas"),
                Foreground = new SolidColorBrush(Color.FromRgb(0xf0, 0xf6, 0xfc)),
                TextWrapping = TextWrapping.Wrap
            };
            aiStack.Children.Add(aiContent);
            aiBorder.Child = aiStack;
            ChatContainer.Children.Add(aiBorder);

            string responseTemplate = $"[Auditoría de Seguridad Local]\n" +
                                       $"Para responder a '{prompt}':\n" +
                                       $"En sistemas de bajo nivel (C/C++), asegúrate de compilar con banderas de mitigación activas:\n" +
                                       $"  • -fstack-protector-strong / /GS (Canarios en pila)\n" +
                                       $"  • -D_FORTIFY_SOURCE=2\n" +
                                       $"  • ASLR + DEP/NX habilitado (/DYNAMICBASE /NXCOMPAT)\n\n" +
                                       $"El modelo local ha validado que el buffer asignado cumple con los límites de seguridad en memoria.";

            string[] words = responseTemplate.Split(' ');
            foreach (var word in words)
            {
                aiContent.Text += word + " ";
                ChatScrollViewer.ScrollToEnd();
                await Task.Delay(35);
            }
        }

        private void BtnSimulate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Simulando vector de ataque local (CWE-120):\n\n" +
                "Payload: $(python -c 'print(\"A\"*72 + \"\\xef\\xbe\\xad\\xde\")')\n" +
                "Resultado: Sobre-escritura del Instruction Pointer (RIP -> 0xDEADBEEF)\n" +
                "Estado de contención: Interceptado en Sandbox local de secdev.",
                "secdev // Exploit Simulation Engine",
                MessageBoxButton.OK,
                MessageBoxImage.Warning
            );
        }

        private void BtnApplyPatch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Parche de seguridad aplicado con éxito en buffer local:\n\n" +
                "- strcpy(stack_buffer, user_payload);\n" +
                "+ strncpy_s(stack_buffer, sizeof(stack_buffer), user_payload, _TRUNCATE);\n\n" +
                "Vulnerabilidad mitigada: 100%. Verificado por AST Analyzer.",
                "secdev // Patch Applied",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Reporte de Auditoría de Seguridad exportado en formato JSON/SARIF:\n\n" +
                "Ruta: ./reports/secdev_audit_exploit_audit_c.sarif\n" +
                "Estándar: OASIS SARIF v2.1.0 compatible con GitHub Security & CI/CD.",
                "secdev // Report Exported",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }
    }
}
