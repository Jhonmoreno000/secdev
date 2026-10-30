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
        private const string PlaceholderText = "Type a message...";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                BtnMaximize_Click(sender, e);
                return;
            }
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

        private void TxtPrompt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtPrompt.Text == PlaceholderText)
            {
                TxtPrompt.Text = string.Empty;
                TxtPrompt.Foreground = new SolidColorBrush(Color.FromRgb(0xe6, 0xed, 0xf3));
            }
        }

        private void TxtPrompt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtPrompt.Text))
            {
                TxtPrompt.Text = PlaceholderText;
                TxtPrompt.Foreground = new SolidColorBrush(Color.FromRgb(0x4c, 0x5f, 0x72));
            }
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

        private void BtnTemplate_Click(object sender, RoutedEventArgs e)
        {
            TxtPrompt.Text = "Analyze memory offset 0x7FFE0040 and generate ROP chain mitigation";
            TxtPrompt.Foreground = new SolidColorBrush(Color.FromRgb(0xe6, 0xed, 0xf3));
            TxtPrompt.Focus();
            TxtPrompt.CaretIndex = TxtPrompt.Text.Length;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            AppendAssistantCard(
                "❈",
                Color.FromRgb(0x00, 0xff, 0x9d),
                "Deep Symbol & AST Scan Complete:",
                "Scanned 14 symbols in auditorium.py / code.emn.\n• 1 Critical Stack Buffer Overflow (line 8)\n• 0 Outbound network sockets (Air-Gap verified)"
            );
        }

        private void BtnSimulate_Click(object sender, RoutedEventArgs e)
        {
            AppendAssistantCard(
                "⚠",
                Color.FromRgb(0xf5, 0xa6, 0x23),
                "Sandbox Exploit Probe Executed",
                "Target: CoeniMatch::stack_frame (0x7FFE0040)\nPayload: 72 bytes cyclic pattern + 0xDEADBEEF\nStatus: Contained inside isolated secdev ring-buffer sandbox."
            );
        }

        private void BtnApplyPatch_Click(object sender, RoutedEventArgs e)
        {
            AppendAssistantCard(
                "❈",
                Color.FromRgb(0x00, 0xff, 0x9d),
                "Automated Secure Patch Applied",
                "Replaced unbounded buffer assignment with Stack.BufSafeBound() guard.\n✓ AST Verification: 0 remaining high-severity vulnerabilities."
            );
        }

        private void AppendAssistantCard(string icon, Color accentColor, string title, string body)
        {
            var rowGrid = new Grid { Margin = new Thickness(0, 0, 0, 12) };
            rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(36) });
            rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var avatarBorder = new Border
            {
                Width = 26,
                Height = 26,
                CornerRadius = new CornerRadius(13),
                Background = new SolidColorBrush(Color.FromRgb(0x0c, 0x24, 0x1e)),
                BorderBrush = new SolidColorBrush(accentColor),
                BorderThickness = new Thickness(1),
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 2, 0, 0)
            };

            var avatarText = new TextBlock
            {
                Text = icon,
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(accentColor)
            };
            avatarBorder.Child = avatarText;
            Grid.SetColumn(avatarBorder, 0);

            var cardBorder = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0x08, 0x17, 0x15)),
                BorderBrush = new SolidColorBrush(accentColor),
                BorderThickness = new Thickness(2, 1, 1, 1),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(14, 10, 14, 10)
            };

            var stack = new StackPanel();
            stack.Children.Add(new TextBlock
            {
                Text = title,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 12.5,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(accentColor),
                Margin = new Thickness(0, 0, 0, 6)
            });

            stack.Children.Add(new TextBlock
            {
                Text = body,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(0xe6, 0xed, 0xf3)),
                TextWrapping = TextWrapping.Wrap
            });

            cardBorder.Child = stack;
            Grid.SetColumn(cardBorder, 1);

            rowGrid.Children.Add(avatarBorder);
            rowGrid.Children.Add(cardBorder);

            ChatContainer.Children.Add(rowGrid);
            ChatScrollViewer.ScrollToEnd();
        }

        private async void SendMessage()
        {
            string prompt = TxtPrompt.Text.Trim();
            if (string.IsNullOrEmpty(prompt) || prompt == PlaceholderText) return;

            TxtPrompt.Text = string.Empty;

            // 1. Add User Prompt Row (Avatar + Card)
            var userGrid = new Grid { Margin = new Thickness(0, 0, 0, 12) };
            userGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(36) });
            userGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var userAvatar = new Border
            {
                Width = 26,
                Height = 26,
                CornerRadius = new CornerRadius(13),
                Background = new SolidColorBrush(Color.FromRgb(0x10, 0x2b, 0x2a)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x1f, 0x6f, 0x64)),
                BorderThickness = new Thickness(1),
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 2, 0, 0),
                Child = new TextBlock
                {
                    Text = "👤",
                    FontSize = 12,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(0x00, 0xff, 0x9d))
                }
            };
            Grid.SetColumn(userAvatar, 0);

            var userCard = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0x0c, 0x12, 0x19)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x17, 0x22, 0x2d)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(12, 9, 12, 9),
                Child = new TextBlock
                {
                    Text = prompt,
                    FontFamily = new FontFamily("Segoe UI, Consolas"),
                    FontSize = 12.5,
                    Foreground = new SolidColorBrush(Color.FromRgb(0xe6, 0xed, 0xf3)),
                    TextWrapping = TextWrapping.Wrap
                }
            };
            Grid.SetColumn(userCard, 1);

            userGrid.Children.Add(userAvatar);
            userGrid.Children.Add(userCard);
            ChatContainer.Children.Add(userGrid);
            ChatScrollViewer.ScrollToEnd();

            // 2. Add Streaming AI Thought-Process Card
            var aiGrid = new Grid { Margin = new Thickness(0, 0, 0, 12) };
            aiGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(36) });
            aiGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var aiAvatar = new Border
            {
                Width = 26,
                Height = 26,
                CornerRadius = new CornerRadius(13),
                Background = new SolidColorBrush(Color.FromRgb(0x0c, 0x2d, 0x23)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xff, 0x9d)),
                BorderThickness = new Thickness(1),
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 2, 0, 0),
                Child = new TextBlock
                {
                    Text = "❈",
                    FontSize = 13,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(0x00, 0xff, 0x9d))
                }
            };
            Grid.SetColumn(aiAvatar, 0);

            var aiStack = new StackPanel();
            aiStack.Children.Add(new TextBlock
            {
                Text = "Thought-process reasoning:",
                FontFamily = new FontFamily("Segoe UI, Consolas"),
                FontSize = 12.5,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Color.FromRgb(0xe6, 0xed, 0xf3)),
                Margin = new Thickness(0, 0, 0, 6)
            });

            var aiContent = new TextBlock
            {
                Text = "",
                FontFamily = new FontFamily("Consolas"),
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(0xb0, 0xc4, 0xd4)),
                TextWrapping = TextWrapping.Wrap
            };
            aiStack.Children.Add(aiContent);

            var aiCard = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0x08, 0x17, 0x15)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0xff, 0x9d)),
                BorderThickness = new Thickness(2, 1, 1, 1),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(14, 10, 14, 10),
                Child = aiStack
            };
            Grid.SetColumn(aiCard, 1);

            aiGrid.Children.Add(aiAvatar);
            aiGrid.Children.Add(aiCard);
            ChatContainer.Children.Add(aiGrid);

            string response = $"1. Analyzing request: \"{prompt}\"\n" +
                              "2. Inspecting HEX segment 0x7FFE0010..0x7FFE0060 and AST node tree.\n" +
                              "3. Verifying bounds safety and generating zero-overhead memory guard.\n" +
                              "4. ✓ Patch validated in isolated local sandbox (52 tok/s).";

            string[] tokens = response.Split(' ');
            foreach (var token in tokens)
            {
                aiContent.Text += token + " ";
                ChatScrollViewer.ScrollToEnd();
                await Task.Delay(25);
            }
        }
    }
}
