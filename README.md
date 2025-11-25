# Taxi Stories

### 🚕 Game Title: Taxi Stories

**Genre:** Narrative Simulation / Driving Adventure / Slice of Life  
**Team Members:** Itzhak Bista, Adir Ofir  
**Wiki / Design Doc:** https://github.com/Computer-games-development-123/Taxi-Stories/wiki/Formal-Elements-and-Market-Research

---

## 1. Game Overview

Taxi Stories is a narrative simulation game set in the lively streets of Tel Aviv.  
You play as a taxi driver who meets different passengers, drives them across the city, and interacts with them through simple dialogue choices. Driving quality, timing, and attitude all affect your **money** and **reputation**.

This prototype focuses on implementing the **core gameplay loop** of a single taxi driver shift:  
pick up a passenger, drive to their destination, drop them off, react to their feedback, and start a new ride.

---

## 2. Core Loop (Prototype Implementation)

The current Unity prototype implements this loop:

**Pick up passenger → Drive to destination → Get evaluated (money + reputation + ride quality/time) → Short dialogue → Spawn new passenger**

### Player actions in the prototype

- Drive a taxi in a 2D top-down view.  
- Navigate from a **Passenger Pickup Point** to a **Destination Point**.  
- Avoid colliding with obstacles to maintain **ride quality**.  
- Try to reach the destination before a **time limit** to avoid penalties.  
- After each ride:  
  - Earn money.  
  - Gain or lose reputation based on performance.  
  - Choose a dialogue response that further adjusts reputation.  
- Start a new ride with a newly positioned passenger.

---

## 3. Player Stats & Systems (Prototype)

### 3.1 Stats

| Stat             | Description                                   | Affected By                             |
|------------------|-----------------------------------------------|-----------------------------------------|
| **Money**        | Total earnings from completed rides           | Ride completion, quality, time bonus    |
| **Reputation**   | How passengers perceive the driver overall    | Driving quality, time, dialogue choice  |
| **Ride Quality** | “Cleanliness” of driving (0–100)              | Collisions with obstacles               |

### 3.2 Ride Evaluation

At the end of each ride:

- **Base fare** is awarded.  
- **Ride Quality** modifies the reward (bonus/penalty).  
- Reaching the destination **on time** gives extra bonus/reputation.  
- Arriving late → penalty.  
- A **dialogue choice** (polite or rude) adjusts reputation.  
- A **new ride** begins automatically.

---

## 4. Technical Architecture (Unity)

### 4.1 Project Structure

```
Assets/
  Scenes/
    TaxiStories_Prototype.unity

  Scripts/
    Player/
      TaxiController.cs
      TaxiCollisionHandler.cs

    World/
      PassengerPickup.cs
      DestinationPoint.cs

    Managers/
      RideManager.cs

    UI/
      PassengerDialogue.cs

  Prefabs/
    Taxi.prefab
    PassengerPoint.prefab
    DestinationPoint.prefab
```

---

## 5. Main Scripts – Responsibilities

### `TaxiController.cs`
- Handles movement only (input + Rigidbody2D physics).  
- Does **not** handle ride logic.

### `TaxiCollisionHandler.cs`
- Detects collisions with objects tagged `"Obstacle"`.  
- Reports collision penalties to `RideManager`.

### `PassengerPickup.cs`
- Detects taxi entering trigger.  
- Calls `OnPassengerPickedUp()`.  
- Implements `ResetPickup()` so it can be reused for new rides.

### `DestinationPoint.cs`
- Detects taxi arrival.  
- Calls `OnDestinationReached()`.

### `RideManager.cs`
- Tracks: money, reputation, rideQuality, ride timer.  
- Updates UI.  
- Evaluates ride rewards/penalties.  
- Starts new rides and positions passengers.

### `PassengerDialogue.cs`
- Controls end-of-ride dialogue UI.  
- Handles reply buttons and updates reputation.

---

## 6. UML (Text Diagram)

```
                   ┌──────────────────┐
                   │    RideManager   │
                   ├──────────────────┤
                   │ money            │
                   │ reputation       │
                   │ rideQuality      │
                   │ rideTimeLimit    │
                   │ currentRideTime  │
                   ├──────────────────┤
                   │ OnPassengerPickedUp()  │
                   │ OnDestinationReached() │
                   │ OnTaxiCollision()      │
                   │ OnDialogueChoice()     │
                   │ StartNewRide()         │
                   └────────▲───────────────┘
                            │
     ┌──────────────────────┼──────────────────────────┐
     │                      │                           │
┌──────────────────┐ ┌──────────────────┐ ┌─────────────────────┐
│ PassengerPickup  │ │ DestinationPoint │ │ PassengerDialogue   │
├──────────────────┤ ├──────────────────┤ ├─────────────────────┤
│ + OnTriggerEnter │ │ + OnTriggerEnter │ │ + ShowDialogue()    │
│ + ResetPickup()  │ └──────────────────┘ │ + OnPoliteAnswer()  │
└──────────────────┘                      │ + OnRudeAnswer()    │
                                          └────────▲────────────┘
                                                   │
                                                   │
                                            ┌──────┴───────────┐
                                            │   UI (Canvas)    │
                                            │ Money / Rep /    │
                                            │ Quality / Timer  │
                                            └──────────────────┘

┌──────────────────┐
│  TaxiController  │
├──────────────────┤
│ + movement input │
│ + physics (2D)   │
└────────▲─────────┘
         │
┌──────────────────┐
│TaxiCollisionHand.│
├──────────────────┤
│ OnCollisionEnter │
│ → OnTaxiCollision│
└──────────────────┘
```

---

## 7. Current Prototype Features

- 2D taxi driving with rotation-based steering.  
- Passenger pickup and drop-off flow.  
- Money + reputation system.  
- Ride quality penalties for collisions.  
- Per-ride timer with bonuses/penalties.  
- Dialogue choice after each ride.  
- Continuous loop:  
  **New passenger → Pick up → Drive → Evaluate → Dialogue → New passenger**

---

## 8. How to Run

1. Open **Unity 2022 or newer**.  
2. Load the project folder from this repository.  
3. Open the scene:  
   `Assets/Scenes/TaxiStories_Prototype.unity`  
4. Press **Play**.

### Controls:
- **W/S or Up/Down** – Accelerate / Reverse  
- **A/D or Left/Right** – Steer

---


## 9. Future Features (Design Targets)

- Larger Tel Aviv world  
- Passenger personalities and moods  
- Multi-branch dialogue system  
- Weather and traffic events  
- Taxi upgrades and maintenance  
- Sound, music, visual polish  

---
