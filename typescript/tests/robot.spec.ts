import { CompassDirection } from "../src/enums";
import Robot from "../src/robot";
import Tabletop from "../src/tabletop";

test("initial status of robot", () => {
  const robot = new Robot(new Tabletop({ rows: 5, columns: 5 }));
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(0);
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.North
  );
});

test("robot movement forward", () => {
  const robot = new Robot(new Tabletop({ rows: 5, columns: 5 }));
  robot.step(1);
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(1);
});

test("robot movement backward", () => {
  const robot = new Robot(new Tabletop({ rows: 5, columns: 5 }));
  robot.step(2);
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(2);

  robot.turnLeft();
  robot.turnLeft();
  robot.step(1);
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(1);
});

test("robot left turn movement", () => {
  const robot = new Robot(new Tabletop({ rows: 5, columns: 5 }));
  robot.step(1);
  robot.turnLeft();
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(1);
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.West
  );

  robot.turnLeft();
  robot.step(1);
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(0);
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.South
  );

  robot.turnLeft();
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.East
  );

  robot.turnLeft();
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.North
  );
});

test("robot right turn movement", () => {
  const robot = new Robot(new Tabletop({ rows: 5, columns: 5 }));
  robot.step(1);
  robot.turnRight();
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(1);
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.East
  );

  robot.turnRight();
  robot.step(1);
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(0);
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.South
  );
});

test("robot reset", () => {
  const robot = new Robot(new Tabletop({ rows: 5, columns: 5 }));
  robot.step(1);
  robot.turnRight();
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(1);
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.East
  );

  robot.turnRight();
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.South
  );

  robot.reset();
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(0);
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.North
  );
});

test("robot orientation", () => {
  const robot = new Robot(new Tabletop({ rows: 5, columns: 5 }));
  robot.step(2);
  robot.turnRight();
  expect(robot.getCurrentOrientation().position.x).toEqual(0);
  expect(robot.getCurrentOrientation().position.y).toEqual(2);
  expect(robot.getCurrentOrientation().direction).toEqual(
    CompassDirection.East
  );
});
