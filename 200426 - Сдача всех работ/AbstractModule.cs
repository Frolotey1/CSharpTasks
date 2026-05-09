namespace Project;

public abstract class AbstractModule {
    public Coord Position;
    public int Fuel;

    protected AbstractModule(Coord position, int fuel) {
        Position = position;
        Fuel = fuel;
    }

    public abstract void Act();

    protected virtual void ConsumeFuel(int amount) {
        Fuel -= amount;
        if (Fuel < 0) {
            Fuel = 0;
        }
    }
}
