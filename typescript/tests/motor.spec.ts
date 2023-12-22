import { MotorMovement } from "../src/enums";
import Motor from "../src/motor";

test("initial motor settings", () => {
  const motor = new Motor();
  expect(motor.currentStep()).toEqual(0);
  expect(motor.currentMovement()).toEqual(MotorMovement.Forward);
});

test("set motor direction to forward", () => {
  const motor = new Motor();
  motor.changeMovement(MotorMovement.Forward);
  expect(motor.currentMovement()).toEqual(MotorMovement.Forward);
});

test("set motor direction to backward", () => {
  const motor = new Motor();
  motor.changeMovement(MotorMovement.Backward);
  expect(motor.currentMovement()).toEqual(MotorMovement.Backward);
});

test("motor movement forward one step", () => {
  const motor = new Motor();
  motor.step(1);
  expect(motor.currentStep()).toEqual(1);
});

test("motor movement backward one step", () => {
  const motor = new Motor();
  motor.changeMovement(MotorMovement.Backward);
  motor.step(1);
  expect(motor.currentStep()).toEqual(-1);
});
