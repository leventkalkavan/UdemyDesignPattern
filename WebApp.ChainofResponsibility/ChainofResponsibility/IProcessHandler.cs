namespace WebApp.ChainofResponsibility.ChainofResponsibility;

public interface IProcessHandler
{
    IProcessHandler SetNext(IProcessHandler processHandler);

    Object handle(Object o);
}