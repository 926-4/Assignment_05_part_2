using System.Data;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;

namespace UBB_SE_2024_Team_42.Utils.Functionals
{
    internal class Filters
    {
        static public bool NonNull(object? instanceMaybe) => instanceMaybe is not null;
        static public bool IPostIsIAnswer(IPost ipost) => ipost is IAnswer;
        static public bool IPostIsIComment(IPost ipost) => ipost is IComment;
        static public bool DataRowRepresentsAnswer(DataRow row) => row["type"].ToString() is not null && (row["type"].ToString() == "answer");
        static public bool DataRowRepresentsComment(DataRow row) => row["type"].ToString() is not null && (row["type"].ToString() == "comment");
        static public bool DataRowRepresentsQuestion(DataRow row) => row["type"].ToString() is not null && (row["type"].ToString() == "question");
    }
}
