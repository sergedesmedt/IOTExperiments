const int blueLed = 2;
const int redLed = 6;
const int greenLed = 7;
const int button = 3;

int buttonPressed = 0;

void setup() {
  pinMode(blueLed, OUTPUT);
  pinMode(button, INPUT);
}

void loop() {
  buttonPressed = digitalRead(button);

  if(buttonPressed == HIGH) {
    digitalWrite(blueLed, HIGH);
  }
  else {
    digitalWrite(blueLed, LOW);
  }
}
