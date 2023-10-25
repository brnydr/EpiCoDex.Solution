
public abstract class ComponentFactory
{
  public static string GetImageContainer(string path)
  {
    $"""
      <img src={path}/>
    """
  }
}