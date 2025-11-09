using System;
public interface INpcDialog
{
    public event Action<string, string, string> UpdateDialog;
}
