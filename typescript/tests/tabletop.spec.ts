import Tabletop from "../src/tabletop";

test("position within tabletop", () => {
  const tabletop = new Tabletop({
    rows: 5,
    columns: 5,
  });
  expect(tabletop.contains({ x: 1, y: 2 })).toBe(true);
});

test("position outside tabletop", () => {
  const tabletop = new Tabletop({
    rows: 5,
    columns: 5,
  });
  expect(tabletop.contains({ x: 6, y: 2 })).toBe(false);
});
