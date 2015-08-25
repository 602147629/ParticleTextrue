using UnityEngine;
using System.Collections;

public class ParticleTextrue : MonoBehaviour {

	public ParticleSystem psys;
	public Texture2D texture;

	public float scale = 0.1f;
	public float size = 0.5f;

	public float downScale = 1.0f;
	public float amp = 2.0f;
	public float speedAmp = 2.0f;

	public float particleAlpha = 1.0f;

	private Color32[] textureColors;

	// unity上でfloatを使う時は、1.0 だとエラーになります。1.0f とする必要があります。

	// Use this for initialization
	void Start () {
		// get texture color
		// テクスチャのカラー情報取得するときは、テクスチャのインスペクタでRead/Write Enabledが有効になっている必要があります。
		textureColors = texture.GetPixels32();
	}
	
	// Update is called once per frame
	void Update () {

		// 
		int w = (int)(texture.width * downScale);
		int h = (int)(texture.height * downScale);

		int num = w * h;
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[num];

		float invDownScale = 1 / downScale;
		
		float offsetX = texture.width / 2;
		float offsetY = texture.height / 2;

		particleAlpha = Mathf.PingPong(Time.time, 1.0f); // particle alpha loopAnim (0.0f -> 1.0f -> 0.0f -> 1.0f.... )

		float time = Time.time;

		// update particles param
		for(int i = 0; i < particles.Length; i++){
			ParticleSystem.Particle p = particles[i];
			float x = i % w;
			float y = i / w;

			float v = amp * Mathf.Sin(time * speedAmp + x * 0.1f + y * 0.05f);// animation z

			Vector3 pos = new Vector3( x * invDownScale - offsetX, y * invDownScale - offsetY, v) * scale;
			int colorIndex = (int)( y * invDownScale) * texture.width + (int)( x * invDownScale);
			Color t_color = textureColors[colorIndex];
			t_color.a = particleAlpha;

			p.color = t_color;
			p.position = pos;
			p.size = size;

			particles[i] = p;
		}

		// update particles
		psys.SetParticles(particles, particles.Length);

	}
}
