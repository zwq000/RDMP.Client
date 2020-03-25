using System.Threading.Tasks;

namespace RMDP
{
    public interface IRmdpEventClient<TMsg> where TMsg :IMsgPackage{

        Task Handle(TMsg msg);
    }
}