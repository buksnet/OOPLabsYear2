namespace Trials
{
    public interface IInit
    {
        void Init();
        void RandomInit();

        void Show();
    }

    // Для IClonable
    public interface ICloneable
    {
        object Clone();
    }
}