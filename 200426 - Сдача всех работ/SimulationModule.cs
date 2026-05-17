namespace Project;

public class SimulationEngine {
    public bool TryStep(AbstractModule[][] map, in int turnNumber, out int processedCount, ref int totalFuelConsumed) {
        processedCount = 0;

        if (map == null || map.Length == 0) {
            return false;
        }

        bool hasAnyModule = false;

        for (int i = 0; i < map.Length; i++) {
            if (map[i] == null) {
                continue;
            }

            for (int j = 0; j < map[i].Length; j++) {
                AbstractModule module = map[i][j];
                if (module == null) {
                    continue;
                }

                int oldFuel = module.Fuel;
                module.Act();
                int fuelConsumed = oldFuel - module.Fuel;
                totalFuelConsumed += fuelConsumed;
                processedCount++;
                hasAnyModule = true;
            }
        }

        return hasAnyModule;
    }

    public static void ShiftMapCoords(AbstractModule[][] map, in Coord offset) {
        if (map == null) {
            return;
        }

        for (int i = 0; i < map.Length; i++) {
            if (map[i] == null) {
                continue;
            }

            for (int j = 0; j < map[i].Length; j++) {
                AbstractModule module = map[i][j];
                if (module == null) {
                    continue;
                }

                Coord newPos = module.Position + offset;
                module.Position = newPos;
            }
        }
    }
}
