using System.Data;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;

namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class Filters
    {
        static public bool NonNull(object? instanceMaybe) => instanceMaybe is not null;
        static public bool IPostIsAnswer(IPost ipost) => ipost is Answer;
        static public bool DataRowRepresentsAnswer(DataRow row) => row["type"].ToString() is not null && (row["type"].ToString() == "answer");
        static public bool DataRowRepresentsComment(DataRow row) => row["type"].ToString() is not null && (row["type"].ToString() == "comment");
        static public bool DataRowRepresentsQuestion(DataRow row) => row["type"].ToString() is not null && (row["type"].ToString() == "question");
    }
}
