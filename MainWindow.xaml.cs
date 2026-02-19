using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace CharacterCreator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CreateButton_Click(object sender,
       RoutedEventArgs e)
        {
            // Получаем имя
            string name = NameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите имя персонажа!",
               "Ошибка",
                MessageBoxButton.OK,
               MessageBoxImage.Warning);
                return;
            }
            // Определяем класс
            string characterClass = GetSelectedClass();

            // Получаем выбранные навыки
            List<string> skills = GetSelectedSkills();
            // Формируем результат
            string result = $"=== Персонаж создан ===\n\n";
            result += $"Имя: {name}\n";
            result += $"Класс: {characterClass}\n\n";

            if (skills.Count > 0)
            {
                result += "Навыки:\n";
                foreach (string skill in skills)
                {
                    result += $" • {skill}\n";
                }
            }
            else
            {
                result += "Навыки: не выбраны\n";
            }
            ResultTextBlock.Text = result;
        }

        private void FighterClassChecked(object sender, RoutedEventArgs e)
        {
            SmithingCheck.IsEnabled = true;
            LockpickCheck.IsEnabled = true;
            Background = Brushes.LightSeaGreen;
            AlchemyCheck.IsEnabled = false;
            AlchemyCheck.IsChecked = false;
            PotionCheck.IsEnabled = false;
            PotionCheck.IsChecked = false;
            StealthCheck.IsEnabled = false;
            StealthCheck.IsChecked = false;
        }
        private void ArcherClassChecked(object sender, RoutedEventArgs e)
        {
            SmithingCheck.IsEnabled = true;
            PotionCheck.IsEnabled = true;
            Background = Brushes.MediumSeaGreen;
            StealthCheck.IsEnabled = false;
            StealthCheck.IsChecked = false;
            LockpickCheck.IsEnabled = false;
            LockpickCheck.IsChecked = false;
            AlchemyCheck.IsEnabled = false;
            AlchemyCheck.IsChecked = false;

        }
        private void MageClassChecked(object sender, RoutedEventArgs e)
        {
            AlchemyCheck.IsEnabled = true;
            PotionCheck.IsEnabled = true;
            Background = Brushes.MediumSlateBlue;
            SmithingCheck.IsEnabled = false;
            SmithingCheck.IsChecked = false;
            LockpickCheck.IsEnabled = false;
            LockpickCheck.IsChecked = false;
            StealthCheck.IsEnabled = false;
            StealthCheck.IsChecked = false;
        }
        private void RougeClassChecked(object sender, RoutedEventArgs e)
        {
            LockpickCheck.IsEnabled = true;
            StealthCheck.IsEnabled = true;
            Background = Brushes.MediumPurple;
            SmithingCheck.IsEnabled = false;
            SmithingCheck.IsChecked = false;
            AlchemyCheck.IsEnabled = false;
            AlchemyCheck.IsChecked = false;
            PotionCheck.IsEnabled = false;
            PotionCheck.IsChecked = false;
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем имя
            Background = Brushes.LightSeaGreen;
            NameTextBox.Text = string.Empty;
            // Сбрасываем класс на первый
            WarriorRadio.IsChecked = true;
            // Снимаем все галочки с навыков
            SmithingCheck.IsChecked = false;
            AlchemyCheck.IsChecked = false;
            PotionCheck.IsChecked = false;
            LockpickCheck.IsChecked = false;
            StealthCheck.IsChecked = false;
            CheckBox[] allSkillChecks = { SmithingCheck, AlchemyCheck, PotionCheck, LockpickCheck, StealthCheck };
            foreach (var check in allSkillChecks) check.IsEnabled = false;
            SmithingCheck.IsEnabled = true;
            LockpickCheck.IsEnabled = true;
            // Очищаем результат
            ResultTextBlock.Text = string.Empty;
        }
        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            // Случайное имя
            string[] names = { "Артур", "Мерлин", "Леголас", "Арагорн", "Гэндальф", "Торин", "Боромир", "Фарамир" };
            NameTextBox.Text = names[random.Next(names.Length)];
            // Случайный класс
            RadioButton[] classRadios = { WarriorRadio, MageRadio, ArcherRadio, RogueRadio };

            classRadios[random.Next(classRadios.Length)].IsChecked = true;
            string selectedClass = GetSelectedClass();
            // Случайные навыки (от 1 до 2)

            CheckBox[] allSkillChecks = { SmithingCheck, AlchemyCheck, PotionCheck, LockpickCheck, StealthCheck };
            CheckBox[] skillChecks = new CheckBox[2];

            if (selectedClass.Equals("Лучник"))
            {
                skillChecks[0] = SmithingCheck;
                skillChecks[1] = PotionCheck;
                Background = Brushes.MediumSeaGreen;
            }
            else if (selectedClass.Equals("Маг"))
            {
                skillChecks[0] = PotionCheck;
                skillChecks[1] = AlchemyCheck;
                Background = Brushes.MediumSlateBlue;
            }
            else if (selectedClass.Equals("Воин"))
            {
                skillChecks[0] = SmithingCheck;
                skillChecks[1] = LockpickCheck;
                Background = Brushes.LightSeaGreen;
            }
            else
            {
                skillChecks[0] = StealthCheck;
                skillChecks[1] = LockpickCheck;
                Background = Brushes.MediumPurple;
            }
            // Сначала сбрасываем все
            foreach (var check in allSkillChecks)
            {
                check.IsChecked = false;
                check.IsEnabled = false;
            }

            // Выбираем случайное количество навыков
            int skillCount = random.Next(1, 3);
            var shuffled = skillChecks.OrderBy(x => random.Next()).Take(skillCount);

            foreach (var check in shuffled)
                check.IsChecked = true;
            foreach (var check in skillChecks) check.IsEnabled = true;
        }
        private string GetSelectedClass()
        {
            if (WarriorRadio.IsChecked == true) return
           "Воин";
            if (MageRadio.IsChecked == true) return "Маг";
            if (ArcherRadio.IsChecked == true) return
           "Лучник";
            if (RogueRadio.IsChecked == true) return "Вор";
            return "Не выбран";
        }
        private List<string> GetSelectedSkills()
        {
            List<string> skills = new List<string>();
            if (SmithingCheck.IsChecked == true)
                skills.Add("Кузнечное дело");
            if (AlchemyCheck.IsChecked == true)
                skills.Add("Алхимия");
            if (PotionCheck.IsChecked == true)
                skills.Add("Зельеварение");
            if (LockpickCheck.IsChecked == true)
                skills.Add("Взлом замков");
            if (StealthCheck.IsChecked == true)
                skills.Add("Скрытность");
            return skills;
        }

    }
}