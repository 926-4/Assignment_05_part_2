using UBB_SE_2024_Team_42.Domain.Reactions;

namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public class ReactionBuilder : AbstractEntityBuilder<IReaction, Reaction>
    {
        public override ReactionBuilder Begin()
            => (ReactionBuilder)base.Begin();

        public ReactionBuilder SetReactionValue(int value)
        {
            instance.Value = value;
            return this;
        }

        public ReactionBuilder SetReacterUserId(long userId)
        {
            instance.UserID = userId;
            return this;
        }
    }
}
