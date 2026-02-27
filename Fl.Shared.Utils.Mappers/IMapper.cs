namespace Fl.Shared.Utils.Mappers;

public interface IMapper<in T1, out T2>
{
    T2 Map(T1 item);
}
