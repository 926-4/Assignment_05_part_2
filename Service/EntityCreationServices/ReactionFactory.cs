using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal class ReactionFactory : AbstractEntityFactory<IReaction, Reaction>
    {
        public override ReactionFactory Begin() 
            => (ReactionFactory)base.Begin();

        public ReactionFactory SetReactionValue(int value)
        {
            instance.Value = value;
            return this;
        }

        public ReactionFactory SetReacterUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }
    }
}
