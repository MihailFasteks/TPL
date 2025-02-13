using System.Text.Json;
using System.Threading.Tasks;

namespace TPL
{
    public partial class Form1 : Form
    {
        List<Student> students = new List<Student>();
        public Form1()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task tsk1 = new Task(Writer);


            Task taskCont = tsk1.ContinueWith((t) => Reader(t), TaskScheduler.FromCurrentSynchronizationContext());

            
            tsk1.Start();

            
            tsk1.Wait(); 

        }
        private void Writer()
        {
            using (StreamWriter writer = new StreamWriter("C:\\Users\\fasta\\Desktop\\PROG\\C#\\2 semestr\\HW\\TPL\\TPL\\bin\\Debug\\net8.0-windows\\Student.json", false))
            {
                students.Add(new Student(textBox1.Text, textBox2.Text, textBox4.Text, textBox3.Text));
                string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
                writer.Write(json);
            }
        }
        private void Reader(Task t)
        {
           
            
            using (StreamReader reader = new StreamReader("C:\\Users\\fasta\\Desktop\\PROG\\C#\\2 semestr\\HW\\TPL\\TPL\\bin\\Debug\\net8.0-windows\\Student.json"))
            {
               
                string readJson = reader.ReadToEnd();
              
               
                if (string.IsNullOrEmpty(readJson))
                {
                    MessageBox.Show("Файл пустой или данные в нем невалидны.");
                    return; 
                }
             
                
                List<Student> loadedStuds = null;
                try
                {
                    loadedStuds = JsonSerializer.Deserialize<List<Student>>(readJson);
                }
                catch (JsonException ex)
                {
                    MessageBox.Show($"Ошибка десериализации: {ex.Message}");
                    return; 
                }
                
              
                if (loadedStuds == null || loadedStuds.Count == 0)
                {
                    MessageBox.Show("Не удалось десериализовать данные или список студентов пуст.");
                    return; 
                }
                listBox1.Items.Clear();
               
                foreach (var stud in loadedStuds)
                {
                    listBox1.Items.Add($"{stud.LName} {stud.Name} - {stud.Age} - {stud.Group}");
                }

               
                var studMinAge = loadedStuds.MinBy(s =>int.Parse(s.Age)); 

                
                if (studMinAge != null)
                {
                    MessageBox.Show($"Самый молодой: {studMinAge.LName} {studMinAge.Name} - {studMinAge.Age} - {studMinAge.Group}");
                }
            }
        }
    }
}
