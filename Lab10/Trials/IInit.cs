namespace Trials
{
    public interface IInit
    {
        void Init();
        void RandomInit();

        void Show();
    }

    public interface ICloneable
    {
        object Clone();
    }
}