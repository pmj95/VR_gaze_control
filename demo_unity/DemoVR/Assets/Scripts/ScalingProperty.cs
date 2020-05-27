using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// Enumeration scaling.
/// avalable values:
/// Large = 0
/// Medium = 1
/// Small = 2
/// </summary>
public enum Scaling
{
    Large = 0,
    Medium = 1,
    Small = 2
}

/// <summary>
/// scaling property
/// </summary>
public class ScalingProperty
{
    public static event PropertyChangedEventHandler PropertyChanged;

    private static Scaling sc;
    private static float sw;
    private static float sh;
    private static float sd;
    
    /// <summary>
    /// getter and setter for the scale width
    /// </summary>
    public static float scaleWidth
    {
        get { return sw; }
        private set
        {
            sw = value;
        }
    }

    /// <summary>
    /// getter and setter for the scale height
    /// </summary>
    public static float scaleHeight
    {
        get { return sh; }
        private set
        {
            sh = value;
        }
    }

    /// <summary>
    /// getter and setter for the scale depth
    /// </summary>
    public static float scaleDepth
    {
        get { return sd; }
        private set
        {
            sd = value;
        }
    }

    /// <summary>
    /// getter and setter for the current scaling
    /// </summary>
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
                scaleHeight = 0.75f;
                scaleWidth = 0.75f;
                scaleDepth = 0.75f;
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

    /// <summary>
    /// invokes a property changed event for scalingProperty
    /// </summary>
    /// <param name="name"></param>
    private static void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
    }
}
