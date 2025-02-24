using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CodeCraze
{
    public partial class QuizForm : Form
    {
        private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex = 0;
        private int userScore = 0;
        
 
        private string username;

        public QuizForm(string username)
        {
            InitializeComponent();
            this.username = username;
           
        }


        public QuizForm()
        {
            InitializeComponent();


        }

        SqlConnection con = new SqlConnection("Data Source=LAPTOP-ILQIOSCA\\SQLEXPRESS;Initial Catalog=codecraze;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                main.Instance.Show();
                this.Hide();
            }

        }

        private void QuizForm_Load(object sender, EventArgs e)
        {
            quizQuestions = FetchQuizQuestions();
            DisplayQuestion();
        }

        public class QuizQuestion
        {
            public int QuestionID { get; set; }
            public string QuestionText { get; set; }
            public string CorrectAnswer { get; set; }
            public string Option1 { get; set; }
            public string Option2 { get; set; }
            public string Option3 { get; set; }
            public string Option4 { get; set; }
        }


        private List<QuizQuestion> FetchQuizQuestions()
        {
            string connectionString = "Data Source=LAPTOP-ILQIOSCA\\SQLEXPRESS;Initial Catalog=codecraze;Integrated Security=True";
            string query = "SELECT TOP 10 QuestionID, QuestionText, CorrectAnswer, Option1, Option2, Option3, Option4 FROM QuizQuestions ORDER BY NEWID()";

            List<QuizQuestion> questions = new List<QuizQuestion>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuizQuestion question = new QuizQuestion
                            {
                                QuestionID = Convert.ToInt32(reader["QuestionID"]),
                                QuestionText = reader["QuestionText"].ToString(),
                                CorrectAnswer = reader["CorrectAnswer"].ToString(),
                                Option1 = reader["Option1"].ToString(),
                                Option2 = reader["Option2"].ToString(),
                                Option3 = reader["Option3"].ToString(),
                                Option4 = reader["Option4"].ToString(),
                            };

                            questions.Add(question);
                        }
                    }
                }
            }

            return questions;
        }

        private void DisplayQuestion()
        {
            if (currentQuestionIndex < quizQuestions.Count)
            {
                QuizQuestion currentQuestion = quizQuestions[currentQuestionIndex];

               
                label1.Text = currentQuestion.QuestionText;
                radioButton1.Text = currentQuestion.Option1;
                radioButton2.Text = currentQuestion.Option2;
                radioButton3.Text = currentQuestion.Option3;
                radioButton4.Text = currentQuestion.Option4;

              
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
            }
            else
            {
                MessageBox.Show("Quiz completed!");             

            }
        }

    private void SaveScoreToDatabase(int score, string username)
{
    try
    {
        con.Open();
        SqlCommand checkUsernameCommand = new SqlCommand("SELECT COUNT(*) FROM Users WHERE LOWER(Username) = LOWER(@Username)", con);
        checkUsernameCommand.Parameters.AddWithValue("@Username", username);
        int userCount = (int)checkUsernameCommand.ExecuteScalar();

        if (userCount > 0)
        {
            SqlCommand updateScoreCommand = new SqlCommand("SaveScore", con);
            updateScoreCommand.CommandType = CommandType.StoredProcedure;
            updateScoreCommand.Parameters.AddWithValue("@Username", username);
            updateScoreCommand.Parameters.AddWithValue("@Score", score);

            updateScoreCommand.ExecuteNonQuery();
            MessageBox.Show("Score saved successfully.", "Score Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
        }
        else
        {
            MessageBox.Show("User does not exist. Please check your username.", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally
    {
        con.Close();
    }
}

    private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked)
            {
                QuizQuestion currentQuestion = quizQuestions[currentQuestionIndex];
                string selectedAnswer = string.Empty;

                if (radioButton1.Checked) selectedAnswer = radioButton1.Text;
                else if (radioButton2.Checked) selectedAnswer = radioButton2.Text;
                else if (radioButton3.Checked) selectedAnswer = radioButton3.Text;
                else if (radioButton4.Checked) selectedAnswer = radioButton4.Text;

                bool isCorrect = (selectedAnswer == currentQuestion.CorrectAnswer);

                if (isCorrect)
                {
                    userScore++;
                }

                MessageBox.Show($"Your answer is {(isCorrect ? "correct!" : "wrong!")}\nYour current score: {userScore}\nCorrect Answer: {currentQuestion.CorrectAnswer}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                currentQuestionIndex++;

                if (currentQuestionIndex < quizQuestions.Count)
                {
                    DisplayQuestion();
                }
                else
                {
                    MessageBox.Show($"Quiz completed! Your final score: {userScore}", "Quiz Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (currentQuestionIndex >= quizQuestions.Count)
                    {
                        
                        DialogResult saveScoreResult = MessageBox.Show("Do you want to save your score to your account?", "Save Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (saveScoreResult == DialogResult.Yes)
                        {
                            SaveScoreToDatabase(userScore, username);
                        }

                        DialogResult result = MessageBox.Show("Do you want to Try Again?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            currentQuestionIndex = 0;
                            userScore = 0;
                            DisplayQuestion();

                        }
                        else
                        {                                           
                            main.Instance.Show();
                            this.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an option before moving to the next question.");
            }
        }



       
    }
}
