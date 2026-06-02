using Microsoft.Data.Sqlite;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            Console.WriteLine("🔥 REPOSITORY CONSTRUCTOR CALLED");
            Console.WriteLine("DB PATH: " + _connectionString); 
        }

        // Get all students
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Students";

                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                RollNo = reader.GetString(2),
                                Email = reader.GetString(3),
                                Phone = reader.GetString(4),
                                Department = reader.GetString(5),
                                Semester = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }

            return students;
        }

        //get Students by Rolllno

        public Student? GetStudentByRollNo(string rollNo)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Students WHERE RollNo = @RollNo";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RollNo", rollNo);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Student
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                RollNo = reader.GetString(2),
                                Email = reader.GetString(3),
                                Phone = reader.GetString(4),
                                Department = reader.GetString(5),
                                Semester = reader.GetInt32(6)
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Add students
        public void AddStudent(Student student)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                INSERT INTO Students 
                (Name, RollNo, Email, Phone, Department, Semester)
                VALUES 
                (@Name, @RollNo, @Email, @Phone, @Department, @Semester)";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@RollNo", student.RollNo);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@Phone", student.Phone);
                    command.Parameters.AddWithValue("@Department", student.Department);
                    command.Parameters.AddWithValue("@Semester", student.Semester);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete students
        public bool DeleteStudentByRollNo(string rollNo)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Students WHERE RollNo = @RollNo";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RollNo", rollNo);

                    int rows = command.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }




        // Update students
        public bool UpdateStudent(Student student)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string query = @"
        UPDATE Students
        SET
            Name = @Name,
            RollNo = @RollNo,
            Email = @Email,
            Phone = @Phone,
            Department = @Department,
            Semester = @Semester
        WHERE Id = @Id";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@RollNo", student.RollNo);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@Phone", student.Phone);
                    command.Parameters.AddWithValue("@Department", student.Department);
                    command.Parameters.AddWithValue("@Semester", student.Semester);

                    int rows = command.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }




        //update by rollno

        public bool UpdateByRollNo(string rollNo, Student student)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string query = @"
        UPDATE Students
        SET
            Name = @Name,
            Email = @Email,
            Phone = @Phone,
            Department = @Department,
            Semester = @Semester
        WHERE RollNo = @RollNo";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RollNo", rollNo);
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@Phone", student.Phone);
                    command.Parameters.AddWithValue("@Department", student.Department);
                    command.Parameters.AddWithValue("@Semester", student.Semester);

                    int rows = command.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }


    }
}