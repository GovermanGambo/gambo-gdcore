using System.Threading.Tasks;

namespace Gambo.GDCore.Transitions;

public interface ITransition
{
    float Duration { get; set; }
    Task StartTransition();
    Task EndTransition();
}