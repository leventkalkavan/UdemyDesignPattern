namespace WebApp.ChainofResponsibility.ChainofResponsibility;

public abstract class ProcessHandler: IProcessHandler
{
    private IProcessHandler _processHandler;

    public IProcessHandler SetNext(IProcessHandler processHandler)
    {
        _processHandler = processHandler;
        return _processHandler;
    }

    public virtual object handle(object o)
    {
        if (_processHandler!=null)
        {
            return _processHandler.handle(o);
        }

        return null;
    }
}