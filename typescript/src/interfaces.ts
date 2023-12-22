import { CommandType, CompassDirection } from "./enums";

export interface Dimension {
  rows: number;
  columns: number;
}

export interface Position {
  x: number;
  y: number;
}

export interface Orientation {
  position: Position;
  direction: CompassDirection;
}

export interface CommandData {
  name: string;
  orientation?: Orientation;
}

export interface CommandHandlers {
  successCallback?: () => void;
  failureCallback?: () => void;
}

export interface CommandParameter {
  orientation: Orientation;
}

export interface Command {
  execute(parameter: CommandParameter, handlers: CommandHandlers): void;
}

export interface CommandSet {
  enabled: boolean;
  command: Command;
  handlers?: CommandHandlers;
}
