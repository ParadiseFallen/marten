using Marten.Linq.Members;
using Weasel.Postgresql;
using Weasel.Postgresql.SqlGeneration;

namespace Marten.Linq.SqlGeneration.Filters;

public class IsNullFilter: IReversibleWhereFragment
{
    public IsNullFilter(IQueryableMember member)
    {
        Member = member;
    }

    public IQueryableMember Member { get; }

    public void Apply(CommandBuilder builder)
    {
        builder.Append(Member.NullTestLocator);
        builder.Append(" is null");
    }

    public bool Contains(string sqlText)
    {
        return Member.Contains(sqlText);
    }

    public ISqlFragment Reverse()
    {
        return new IsNotNullFilter(Member);
    }
}
