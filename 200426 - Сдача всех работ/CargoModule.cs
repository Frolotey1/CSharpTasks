namespace Project;

public sealed class CargoModule : AbstractModule {
    public CargoModule(Coord position, int fuel) : base(position, fuel) {}

    public override void Act() {
        if (Fuel <= 0) {
            return;
        }

        Position = new Coord(Position.X, Position.Y - 1);
        ConsumeFuel(2);
    }
}
