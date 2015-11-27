// MonoGame UI Touch Prototype
// Written by D. Sinclair, 2015
// ==============================
// Player.cs

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Collisions {
	class Player {
	// Member methods
		public Player(Texture2D texture, Vector2 position, float rotation, float speed, Texture2D circleTexture, Vector2 circleStart) {
			m_car = new Sprite(texture, position, rotation);
	
			m_speed = speed;
            m_acceleration = 0.05f;

			m_circleStart = circleStart;
			m_circle = new Sprite(circleTexture, circleStart, 0);

            //rectangle used for detecting collsions
            m_carBounds = new Rectangle(100, 100, 50, 100);
		}

		// Getters
		public void Update() {
			// If no active touch
			if(!m_set) {
				m_circle.SetPosition(m_circleStart);
			}


			// Check if car is braking
			if(m_braking) {
				// Decelerate
				if(m_speed > 0) {
                    m_speed -= m_acceleration;
				} else if(m_speed < 0) {
					m_speed = 0;
				}
			} else {
				// Accelerate
				if(m_speed < MAX_SPEED) {
					m_speed += m_acceleration;
				} else if(m_speed > MAX_SPEED) {
					m_speed = MAX_SPEED;
				}
			}

			// Move car forward
			 direction = new Vector2((float)Math.Cos(m_car.GetRotationRadians()), (float)Math.Sin(m_car.GetRotationRadians()));

			direction.Normalize();

			m_car.SetPosition(m_car.GetPosition() + direction * m_speed);

			m_braking = false;
			m_set = false;
			m_steeringValue = 0;
		}

		public float GetSpeed() {
			return m_speed;
		}

		public bool GetBraking() {
			return m_braking;
		}

		// Setters
		public void SetSpeed(float speed) {
			m_speed = speed;
		}

		public void SetBraking(bool braking) {
			m_braking = braking;
		}

	    //Member variables
		public Sprite m_car;
	
		private float m_speed;
        private float m_acceleration;

		private bool m_braking;
		public float m_steeringValue;
		public bool m_set;
		private const float MAX_SPEED = 10;
		public float STEERING_RANGE = 200;

		public Sprite m_circle;
		public Vector2 m_circleStart;
        public Vector2 direction;
        public Rectangle m_carBounds;
	}
}
