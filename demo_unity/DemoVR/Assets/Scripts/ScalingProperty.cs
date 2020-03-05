using System.ComponentModel;
using System.Runtime.CompilerServices;

public enum Scaling
{
    Large = 0,
    Medium = 1,
    Small = 2
}

public class ScalingProperty
{
    public static event PropertyChangedEventHandler PropertyChanged;

    private static Scaling sc;
    private static float sw;
    private static float sh;
    private static float sd;

    public static float scaleWidth
    {
        get { return sw; }
        private set
        {
            sw = value;
        }
    }

    public static float scaleHeight
    {
        get { return sh; }
        private set
        {
            sh = value;
        }
    }

    public static float scaleDepth
    {
        get { return sd; }
        private set
        {
            sd = value;
        }
    }

    public static Scaling currentScaling
    {
        get { return sc; }
        set
        {
            sc = value;

            if (sc == Scaling.Small)
            {
                scaleHeight = 0.5f;
                scaleWidth = 0.5f;
                scaleDepth = 0.5f;
            }
            else if (sc == Scaling.Medium)
            {
                scaleHeight = 0.707f;
                scaleWidth = 0.707f;
                scaleDepth = 0.707f;
            }
            else
            {
                scaleHeight = 1.0f;
                scaleWidth = 1.0f;
                scaleDepth = 1.0f;
            }

            // Call OnPropertyChanged whenever the property is updated
            OnPropertyChanged();
        }
    }

    private static void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
    }
}
