using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AetherTemp.Menu
{
    class Particles
    {
        public static void CreateDomain2()
        {
            Vector3 spawnPosition = new Vector3(-63.2589f, 9.4352f, -65.2775f);
            for (int i = 0; i < 1; i++)
            {
                GameObject lineObject = new GameObject("LineRenderer_" + i);
                LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
                Color lineColor = GetRandomColor();
                lineRenderer.startColor = lineColor;
                lineRenderer.endColor = lineColor;
                lineRenderer.positionCount = 2;

                Vector3 startPosition = spawnPosition + new Vector3(
                    UnityEngine.Random.Range(-25f, 25f),
                    UnityEngine.Random.Range(-25f, 25f),
                    UnityEngine.Random.Range(-25f, 25f)
                );

                Vector3 endPosition = spawnPosition + new Vector3(
                    UnityEngine.Random.Range(-25f, 25f),
                    UnityEngine.Random.Range(-10f, 10f),
                    UnityEngine.Random.Range(-25f, 25f)
                );

                lineRenderer.SetPosition(0, startPosition);
                lineRenderer.SetPosition(1, endPosition);

                UnityEngine.Object.Destroy(lineObject, 1.5f);
            }

            for (int i = 0; i < 2; i++)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = spawnPosition + new Vector3(
                    UnityEngine.Random.Range(-25f, 25f),
                    UnityEngine.Random.Range(-25f, 25f),
                    UnityEngine.Random.Range(-25f, 25f)
                );
                sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                sphere.GetComponent<Collider>().enabled = false;

                Color sphereColor = GetRandomColor();
                sphere.GetComponent<Renderer>().material.color = sphereColor;

                UnityEngine.Object.Destroy(sphere, 2f);
            }
        }

        public static void CreateFireEffect()
        {
            CreateFireAtPosition(GorillaTagger.Instance.leftHandTransform.position);
            CreateFireAtPosition(GorillaTagger.Instance.rightHandTransform.position);
        }

        private static void CreateFireAtPosition(Vector3 position)
        {
            GameObject fireEffect = new GameObject("FireEffect");
            fireEffect.transform.position = position;

            ParticleSystem fireParticles = fireEffect.AddComponent<ParticleSystem>();
            ParticleSystem.MainModule mainModule = fireParticles.main;

            mainModule.startColor = new ParticleSystem.MinMaxGradient(Color.red, Color.black);
            mainModule.startSize = 0.05f;
            mainModule.startSpeed = 0.25f;
            mainModule.startLifetime = 1.5f;
            mainModule.loop = true;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
            mainModule.maxParticles = 30;

            ParticleSystemRenderer particleRenderer = fireParticles.GetComponent<ParticleSystemRenderer>();
            particleRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
            particleRenderer.material.SetColor("_Color", Color.red);

            ParticleSystem.EmissionModule emission = fireParticles.emission;
            emission.rateOverTime = 5f;

            ParticleSystem.ShapeModule shape = fireParticles.shape;
            shape.shapeType = ParticleSystemShapeType.Cone;
            shape.angle = 20f;
            shape.radius = 0.1f;

            UnityEngine.Object.Destroy(fireEffect, 0.5f);
        }


        public static void CreateBlackHole()
        {
            Vector3 blackHolePosition = new Vector3(-63.2589f, 9.4352f, -65.2775f);
            GameObject blackHoleEffect = new GameObject("BlackHoleEffect");
            blackHoleEffect.transform.position = blackHolePosition;
            ParticleSystem blackHoleParticles = blackHoleEffect.AddComponent<ParticleSystem>();
            ParticleSystem.MainModule mainModule = blackHoleParticles.main;

            mainModule.startColor = new ParticleSystem.MinMaxGradient(
                new Color(0f, 0f, 0f),
                new Color(0.1f, 0.1f, 0.1f)
            );
            mainModule.startSize = 0.4f;
            mainModule.startSpeed = 0.5f;
            mainModule.startLifetime = 2f;
            mainModule.loop = true;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
            mainModule.maxParticles = 150;
            ParticleSystemRenderer renderer = blackHoleParticles.GetComponent<ParticleSystemRenderer>();
            renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
            ParticleSystem.EmissionModule emission = blackHoleParticles.emission;
            emission.rateOverTime = 30f;
            ParticleSystem.ShapeModule shape = blackHoleParticles.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.radius = 2.5f;
            shape.randomDirectionAmount = 0.1f;
            ParticleSystem.RotationOverLifetimeModule rotationModule = blackHoleParticles.rotationOverLifetime;
            rotationModule.z = new ParticleSystem.MinMaxCurve(0.5f, 1.0f);
            ParticleSystem.VelocityOverLifetimeModule velocityModule = blackHoleParticles.velocityOverLifetime;
            velocityModule.x = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocityModule.y = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocityModule.z = new ParticleSystem.MinMaxCurve(-1f, -2f);

            blackHoleParticles.Play();
            UnityEngine.Object.Destroy(blackHoleParticles, 2f);
            Rigidbody playerRigidbody = GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody;

            if (playerRigidbody != null)
            {
                Vector3 directionToBlackHole = blackHolePosition - GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.position;
                float distance = directionToBlackHole.magnitude;
                float pullForce = Mathf.Clamp(1000f / distance, 0f, 10f);
                playerRigidbody.AddForce(directionToBlackHole.normalized * pullForce * Time.deltaTime, ForceMode.VelocityChange);
            }
            Collider[] colliders = Physics.OverlapSphere(blackHolePosition, 10f);

            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 directionToBlackHole = blackHolePosition - collider.transform.position;
                    float distance = directionToBlackHole.magnitude;
                    float pullForce = Mathf.Clamp(1000f / distance, 0f, 10f);
                    rb.AddForce(directionToBlackHole.normalized * pullForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
        }



        public static void CreateWhiteHole()
        {
            Vector3 whiteHolePosition = new Vector3(-63.2589f, 9.4352f, -65.2775f);

            GameObject whiteHoleEffect = new GameObject("WhiteHoleEffect");
            whiteHoleEffect.transform.position = whiteHolePosition;

            ParticleSystem whiteHoleParticles = whiteHoleEffect.AddComponent<ParticleSystem>();
            ParticleSystem.MainModule mainModule = whiteHoleParticles.main;

            mainModule.startColor = new ParticleSystem.MinMaxGradient(
                new Color(1f, 1f, 1f),
                new Color(0.9f, 0.9f, 0.9f)
            );

            mainModule.startSize = 0.4f;
            mainModule.startSpeed = 0.5f;
            mainModule.startLifetime = 2f;
            mainModule.loop = true;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
            mainModule.maxParticles = 150;

            ParticleSystemRenderer renderer = whiteHoleParticles.GetComponent<ParticleSystemRenderer>();
            renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));

            ParticleSystem.EmissionModule emission = whiteHoleParticles.emission;
            emission.rateOverTime = 30f;

            ParticleSystem.ShapeModule shape = whiteHoleParticles.shape;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.radius = 2.5f;
            shape.randomDirectionAmount = 0.1f;

            ParticleSystem.RotationOverLifetimeModule rotationModule = whiteHoleParticles.rotationOverLifetime;
            rotationModule.z = new ParticleSystem.MinMaxCurve(0.5f, 1.0f);

            ParticleSystem.VelocityOverLifetimeModule velocityModule = whiteHoleParticles.velocityOverLifetime;
            velocityModule.x = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocityModule.y = new ParticleSystem.MinMaxCurve(0f, 0f);
            velocityModule.z = new ParticleSystem.MinMaxCurve(1f, 2f);

            whiteHoleParticles.Play();
            UnityEngine.Object.Destroy(whiteHoleParticles, 2f);

            Rigidbody playerRigidbody = GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody;

            if (playerRigidbody != null)
            {
                Vector3 directionToWhiteHole = playerRigidbody.transform.position - whiteHolePosition;
                float distance = directionToWhiteHole.magnitude;

                float pushForce = Mathf.Clamp(1000f / distance, 0f, 10f);

                playerRigidbody.AddForce(directionToWhiteHole.normalized * pushForce * Time.deltaTime, ForceMode.VelocityChange);
            }

            Collider[] colliders = Physics.OverlapSphere(whiteHolePosition, 10f);

            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 directionToWhiteHole = collider.transform.position - whiteHolePosition;
                    float distance = directionToWhiteHole.magnitude;

                    float pushForce = Mathf.Clamp(1000f / distance, 0f, 10f);

                    rb.AddForce(directionToWhiteHole.normalized * pushForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
        }


        public static void CastMagicSpell()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                Vector3 spellStartPosition = GorillaTagger.Instance.rightHandTransform.position;
                Quaternion spellStartRotation = GorillaTagger.Instance.rightHandTransform.rotation;

                GameObject magicSpell = new GameObject("MagicSpellEffect");
                magicSpell.transform.position = spellStartPosition;

                ParticleSystem spellParticles = magicSpell.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule mainModule = spellParticles.main;

                mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(0.2f, 0.3f, 1f), new Color(0.6f, 0.8f, 1f));
                mainModule.startSize = 0.05f;
                mainModule.startSpeed = 10f;
                mainModule.startLifetime = 2f;
                mainModule.loop = false;
                mainModule.simulationSpace = ParticleSystemSimulationSpace.World;

                ParticleSystem.EmissionModule emission = spellParticles.emission;
                emission.rateOverTime = 0f;
                emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0f, 20) });

                ParticleSystem.ShapeModule shape = spellParticles.shape;
                shape.shapeType = ParticleSystemShapeType.Cone;
                shape.angle = 15f;
                shape.radius = 0.5f;

                spellParticles.transform.rotation = spellStartRotation;

                ParticleSystem.VelocityOverLifetimeModule velocityModule = spellParticles.velocityOverLifetime;
                velocityModule.x = new ParticleSystem.MinMaxCurve(0f);
                velocityModule.y = new ParticleSystem.MinMaxCurve(0f);
                velocityModule.z = new ParticleSystem.MinMaxCurve(10f);

                ParticleSystemRenderer renderer = spellParticles.GetComponent<ParticleSystemRenderer>();
                renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
                renderer.material.SetColor("_Color", new Color(0.2f, 0.3f, 1f));

                spellParticles.Play();
                UnityEngine.Object.Destroy(magicSpell, 2f);
            }
        }

        private static Color GetRandomColor()
        {
            return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }





        private static bool isIceSpearCast = false;

        public static void SwordSlash()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton && !isIceSpearCast)
            {
                isIceSpearCast = true;
                Vector3 spellStartPosition = GorillaTagger.Instance.rightHandTransform.position;
                Quaternion spellStartRotation = GorillaTagger.Instance.rightHandTransform.rotation;
                GameObject iceSpear = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                iceSpear.transform.position = spellStartPosition;
                iceSpear.transform.rotation = spellStartRotation;
                iceSpear.transform.localScale = new Vector3(0.3f, 2f, 0.3f);
                Rigidbody iceSpearRb = iceSpear.AddComponent<Rigidbody>();
                iceSpearRb.useGravity = false;
                iceSpearRb.AddForce(spellStartRotation * Vector3.forward * iceSpearSpeed, ForceMode.VelocityChange);
                ParticleSystem iceParticles = iceSpear.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule mainModule = iceParticles.main;
                mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(0.5f, 0.8f, 1f), new Color(0.2f, 0.5f, 1f));
                mainModule.startSize = 0.3f;
                mainModule.startLifetime = 1.5f;
                mainModule.startSpeed = 1f;
                ParticleSystem.EmissionModule emission = iceParticles.emission;
                emission.rateOverTime = 40f;
                ParticleSystem.TrailModule trails = iceParticles.trails;
                trails.enabled = true;
                trails.widthOverTrail = 0.2f;
                ParticleSystem.ShapeModule shape = iceParticles.shape;
                shape.shapeType = ParticleSystemShapeType.Cone;
                shape.angle = 5f;
                ParticleSystemRenderer renderer = iceParticles.GetComponent<ParticleSystemRenderer>();
                renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
                UnityEngine.Object.Destroy(iceSpear, 5f);
            }
            if (!ControllerInputPoller.instance.rightControllerPrimaryButton && isIceSpearCast)
            {
                isIceSpearCast = false;
            }
        }

        private static float iceSpearSpeed = 15f;

        private static bool hasCastLightning = false;

        private static bool isFireballCast = false;
        public static void CastFireballMagic()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton && !isFireballCast)
            {
                isFireballCast = true;
                Vector3 spellStartPosition = GorillaTagger.Instance.rightHandTransform.position;
                Quaternion spellStartRotation = GorillaTagger.Instance.rightHandTransform.rotation;
                GameObject fireball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                fireball.transform.position = spellStartPosition;
                fireball.transform.rotation = spellStartRotation;
                fireball.transform.localScale = new Vector3(1f, 1f, 1f);
                Rigidbody fireballRb = fireball.AddComponent<Rigidbody>();
                fireballRb.useGravity = false;
                fireballRb.AddForce(spellStartRotation * Vector3.forward * fireballSpeed, ForceMode.VelocityChange);
                ParticleSystem fireParticles = fireball.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule mainModule = fireParticles.main;
                mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(1f, 0.3f, 0f), new Color(1f, 0.6f, 0f));
                mainModule.startSize = 0.5f;
                mainModule.startLifetime = 2f;
                mainModule.startSpeed = 1f;
                ParticleSystem.EmissionModule emission = fireParticles.emission;
                emission.rateOverTime = 50f;
                emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0f, 50) });
                ParticleSystem.ShapeModule shape = fireParticles.shape;
                shape.shapeType = ParticleSystemShapeType.Sphere;
                shape.radius = 0.5f;
                ParticleSystemRenderer renderer = fireParticles.GetComponent<ParticleSystemRenderer>();
                renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
                UnityEngine.Object.Destroy(fireball, 5f);
            }
            if (!ControllerInputPoller.instance.rightControllerPrimaryButton && isFireballCast)
            {
                isFireballCast = false;
            }
        }

        private static float fireballSpeed = 10f;

        public static void CastSparkMagic()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                Vector3 spellStartPosition = GorillaTagger.Instance.rightHandTransform.position;
                Quaternion spellStartRotation = GorillaTagger.Instance.rightHandTransform.rotation;
                GameObject sparkSpell = new GameObject("SparkSpellEffect");
                sparkSpell.transform.position = spellStartPosition;
                ParticleSystem sparkParticles = sparkSpell.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule mainModule = sparkParticles.main;
                mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(0.8f, 0.8f, 1f), new Color(1f, 0.9f, 0.2f));
                mainModule.startSize = 0.03f;
                mainModule.startSpeed = 8f;
                mainModule.startLifetime = 1.5f;
                mainModule.loop = false;
                mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
                ParticleSystem.EmissionModule emission = sparkParticles.emission;
                emission.rateOverTime = 0f;
                emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0f, 30) });
                ParticleSystem.ShapeModule shape = sparkParticles.shape;
                shape.shapeType = ParticleSystemShapeType.Cone;
                shape.angle = 25f;
                shape.radius = 0.05f;
                sparkParticles.transform.rotation = spellStartRotation;
                ParticleSystem.VelocityOverLifetimeModule velocityModule = sparkParticles.velocityOverLifetime;
                velocityModule.x = new ParticleSystem.MinMaxCurve(-1f, 1f);
                velocityModule.y = new ParticleSystem.MinMaxCurve(0f, 1f);
                velocityModule.z = new ParticleSystem.MinMaxCurve(5f, 10f);
                ParticleSystemRenderer renderer = sparkParticles.GetComponent<ParticleSystemRenderer>();
                renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));

                sparkParticles.Play();

                UnityEngine.Object.Destroy(sparkSpell, 1.5f);
            }
        }

        public static void CastLightMagic()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                Vector3 sparkStartPosition = GorillaTagger.Instance.rightHandTransform.position;
                Quaternion sparkStartRotation = GorillaTagger.Instance.rightHandTransform.rotation;

                GameObject spark = new GameObject("SparkEffect");
                spark.transform.position = sparkStartPosition;

                ParticleSystem sparkParticles = spark.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule mainModule = sparkParticles.main;

                mainModule.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, Color.white);
                mainModule.startSize = 0.05f;
                mainModule.startSpeed = 6f;
                mainModule.startLifetime = 1f;
                mainModule.loop = false;
                mainModule.simulationSpace = ParticleSystemSimulationSpace.World;

                ParticleSystem.EmissionModule emission = sparkParticles.emission;
                emission.rateOverTime = 0f;
                emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0f, 10) });

                ParticleSystem.ShapeModule shape = sparkParticles.shape;
                shape.shapeType = ParticleSystemShapeType.Sphere;
                shape.radius = 0.1f;

                sparkParticles.transform.rotation = sparkStartRotation;

                ParticleSystem.VelocityOverLifetimeModule velocityModule = sparkParticles.velocityOverLifetime;
                velocityModule.x = new ParticleSystem.MinMaxCurve(0f);
                velocityModule.y = new ParticleSystem.MinMaxCurve(0f);
                velocityModule.z = new ParticleSystem.MinMaxCurve(6f);

                ParticleSystemRenderer renderer = sparkParticles.GetComponent<ParticleSystemRenderer>();
                renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
                renderer.material.SetColor("_Color", Color.yellow);

                sparkParticles.Play();
                UnityEngine.Object.Destroy(spark, 1.5f);
            }
        }

        public static void Draw()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                Vector3 spellStartPosition = GorillaTagger.Instance.rightHandTransform.position;
                Quaternion spellStartRotation = GorillaTagger.Instance.rightHandTransform.rotation;

                GameObject rasenganSpell = new GameObject("RasenganEffect");
                rasenganSpell.transform.position = spellStartPosition;

                ParticleSystem rasenganParticles = rasenganSpell.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule mainModule = rasenganParticles.main;
                mainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(0.4f, 0.7f, 1f), new Color(0.9f, 0.4f, 1f));
                mainModule.startSize = 0.05f;
                mainModule.startSpeed = 0.1f;
                mainModule.startLifetime = 2f;
                mainModule.loop = true;
                mainModule.simulationSpace = ParticleSystemSimulationSpace.World;

                ParticleSystem.EmissionModule emission = rasenganParticles.emission;
                emission.rateOverTime = 100f;

                ParticleSystem.ShapeModule shape = rasenganParticles.shape;
                shape.shapeType = ParticleSystemShapeType.Sphere;
                shape.radius = 0.005f;

                ParticleSystem.VelocityOverLifetimeModule velocityModule = rasenganParticles.velocityOverLifetime;
                velocityModule.x = new ParticleSystem.MinMaxCurve(0f, 0f);
                velocityModule.y = new ParticleSystem.MinMaxCurve(0f, 0f);
                velocityModule.z = new ParticleSystem.MinMaxCurve(8f, 12f);

                rasenganParticles.transform.rotation = spellStartRotation;
                rasenganSpell.transform.Rotate(Vector3.up, Time.time * 300f, Space.World);

                ParticleSystemRenderer renderer = rasenganParticles.GetComponent<ParticleSystemRenderer>();
                renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
                rasenganParticles.Play();

                UnityEngine.Object.Destroy(rasenganSpell, 2f);
            }
        }
        public static void CastLightningBolt()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton && !hasCastLightning)
            {
                hasCastLightning = true;
                Vector3 startPosition = GorillaTagger.Instance.rightHandTransform.position;
                Quaternion startRotation = GorillaTagger.Instance.rightHandTransform.rotation;
                Vector3 endPosition = startPosition + (startRotation * Vector3.forward * 10f);
                GameObject lightning = new GameObject("LightningBolt");
                LineRenderer lineRenderer = lightning.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 10;
                lineRenderer.startWidth = 0.3f;
                lineRenderer.endWidth = 0.1f;
                lineRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = new Color(0.5f, 0.5f, 1f);
                for (int i = 0; i < 10; i++)
                {
                    float t = i / 9f;
                    Vector3 point = Vector3.Lerp(startPosition, endPosition, t);
                    point += new Vector3(
                        UnityEngine.Random.Range(-0.3f, 0.3f),
                        UnityEngine.Random.Range(-0.3f, 0.3f),
                        0
                    );
                    lineRenderer.SetPosition(i, point);
                }
                GameObject sparks = new GameObject("LightningSparks");
                sparks.transform.position = endPosition;
                ParticleSystem sparksParticles = sparks.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule main = sparksParticles.main;
                main.startColor = new ParticleSystem.MinMaxGradient(Color.white, Color.cyan);
                main.startSize = 0.5f;
                main.startLifetime = 0.2f;
                main.startSpeed = 2f;
                ParticleSystem.EmissionModule emission = sparksParticles.emission;
                emission.rateOverTime = 50f;
                ParticleSystem.ShapeModule shape = sparksParticles.shape;
                shape.shapeType = ParticleSystemShapeType.Sphere;
                shape.radius = 0.5f;
                ParticleSystemRenderer sparksRenderer = sparksParticles.GetComponent<ParticleSystemRenderer>();
                sparksRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
                sparksParticles.Play();
                UnityEngine.Object.Destroy(lightning, 0.3f);
                UnityEngine.Object.Destroy(sparks, 0.5f);
            }
            if (!ControllerInputPoller.instance.rightControllerPrimaryButton && hasCastLightning)
            {
                hasCastLightning = false;
            }
        }

        public static void CreateNebulaStorm()
        {
            Vector3 nebulaPosition = new Vector3(-63.2589f, 9.4352f, -65.2775f);
            GameObject nebula = new GameObject("NebulaStorm");
            nebula.transform.position = nebulaPosition;
            ParticleSystem mistParticles = nebula.AddComponent<ParticleSystem>();
            ParticleSystem.MainModule mistMain = mistParticles.main;
            mistMain.startColor = new ParticleSystem.MinMaxGradient(
                new Color(0.2f, 0.3f, 0.6f, 0.3f),
                new Color(0.6f, 0.1f, 0.7f, 0.4f)
            );
            mistMain.startSize = 3f;
            mistMain.startLifetime = 4f;
            mistMain.startSpeed = 0f;
            mistMain.loop = true;
            mistMain.simulationSpace = ParticleSystemSimulationSpace.World;
            mistMain.maxParticles = 100;
            ParticleSystem.EmissionModule mistEmission = mistParticles.emission;
            mistEmission.rateOverTime = 5f;
            ParticleSystem.ShapeModule mistShape = mistParticles.shape;
            mistShape.shapeType = ParticleSystemShapeType.Sphere;
            mistShape.radius = 2f;
            ParticleSystemRenderer mistRenderer = mistParticles.GetComponent<ParticleSystemRenderer>();
            mistRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
            GameObject stars = new GameObject("NebulaStars");
            stars.transform.parent = nebula.transform;
            stars.transform.localPosition = Vector3.zero;
            ParticleSystem starParticles = stars.AddComponent<ParticleSystem>();
            ParticleSystem.MainModule starMain = starParticles.main;
            starMain.startColor = new ParticleSystem.MinMaxGradient(Color.white, new Color(1f, 1f, 0.8f));
            starMain.startSize = 0.05f;
            starMain.startLifetime = 2f;
            starMain.startSpeed = 0f;
            starMain.loop = true;
            starMain.simulationSpace = ParticleSystemSimulationSpace.World;
            starMain.maxParticles = 50;
            ParticleSystem.EmissionModule starEmission = starParticles.emission;
            starEmission.rateOverTime = 10f;
            ParticleSystem.ShapeModule starShape = starParticles.shape;
            starShape.shapeType = ParticleSystemShapeType.Sphere;
            starShape.radius = 2.5f;
            ParticleSystemRenderer starRenderer = starParticles.GetComponent<ParticleSystemRenderer>();
            starRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
            mistParticles.Play();
            starParticles.Play();
            UnityEngine.Object.Destroy(nebula, 5f);
        }



        private static float timeSinceLastStrike = 0f;
        private static float strikeInterval = 1f;

        public static void CreateLightningEffect()
        {
            timeSinceLastStrike += Time.deltaTime;
            if (timeSinceLastStrike < strikeInterval)
                return;

            timeSinceLastStrike = 0f;

            Vector3 stormPosition = new Vector3(-63.2589f, 9.4352f, -65.2775f);

            GameObject lightning = new GameObject("LightningStrike");
            lightning.transform.position = stormPosition;

            LineRenderer lineRenderer = lightning.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = new Color(0.5f, 0.5f, 1f);
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.useWorldSpace = true;

            Vector3 startPoint = new Vector3(
                stormPosition.x + UnityEngine.Random.Range(-25, 25),
                stormPosition.y + UnityEngine.Random.Range(10f, 20f),
                stormPosition.z + UnityEngine.Random.Range(-25, 25)
            );
            Vector3 endPoint = new Vector3(
                stormPosition.x + UnityEngine.Random.Range(-10f, 10f),
                stormPosition.y - UnityEngine.Random.Range(10f, 20f),
                stormPosition.z + UnityEngine.Random.Range(-10f, 10f)
            );

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);

            Light flashLight = lightning.AddComponent<Light>();
            flashLight.color = Color.white;
            flashLight.intensity = UnityEngine.Random.Range(4f, 8f);
            flashLight.range = 10f;
            flashLight.shadows = LightShadows.None;

            UnityEngine.Object.Destroy(lightning, UnityEngine.Random.Range(0.1f, 0.5f));

            UnityEngine.Object.Destroy(flashLight, 0.1f);
        }




        private static bool hasCastVoidRift = false;

        public static void CastVoidRift()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton && !hasCastVoidRift)
            {
                hasCastVoidRift = true;

                Vector3 startPosition = GorillaTagger.Instance.rightHandTransform.position;
                Quaternion startRotation = GorillaTagger.Instance.rightHandTransform.rotation;
                Vector3 direction = startRotation * Vector3.forward;

                GameObject rift = new GameObject("VoidRift");
                rift.transform.position = startPosition + (direction * 2f);
                rift.transform.rotation = startRotation;

                ParticleSystem riftParticles = rift.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule main = riftParticles.main;
                main.startColor = new Color(0f, 0f, 0f, 1f);
                main.startSize = 0.6f;
                main.startLifetime = 1.2f;
                main.loop = false;
                main.simulationSpace = ParticleSystemSimulationSpace.World;

                ParticleSystem.ShapeModule shape = riftParticles.shape;
                shape.shapeType = ParticleSystemShapeType.Donut;
                shape.radius = 0.5f;

                ParticleSystem.EmissionModule emission = riftParticles.emission;
                emission.rateOverTime = 30f;

                ParticleSystemRenderer riftRenderer = riftParticles.GetComponent<ParticleSystemRenderer>();
                riftRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
                GameObject lightning = new GameObject("RiftLightning");
                lightning.transform.parent = rift.transform;
                lightning.transform.localPosition = Vector3.zero;
                ParticleSystem lightningParticles = lightning.AddComponent<ParticleSystem>();

                ParticleSystem.MainModule lightningMain = lightningParticles.main;
                lightningMain.startColor = Color.white;
                lightningMain.startSize = 0.15f;
                lightningMain.startLifetime = 0.3f;

                ParticleSystem.EmissionModule lightningEmission = lightningParticles.emission;
                lightningEmission.rateOverTime = 10f;

                ParticleSystem.ShapeModule lightningShape = lightningParticles.shape;
                lightningShape.shapeType = ParticleSystemShapeType.Sphere;
                lightningShape.radius = 0.6f;

                ParticleSystemRenderer lightningRenderer = lightningParticles.GetComponent<ParticleSystemRenderer>();
                lightningRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));

                GameObject sparks = new GameObject("RiftSparks");
                sparks.transform.parent = rift.transform;
                sparks.transform.localPosition = Vector3.zero;
                ParticleSystem sparksParticles = sparks.AddComponent<ParticleSystem>();

                ParticleSystem.MainModule sparksMain = sparksParticles.main;
                sparksMain.startColor = new Color(1f, 0.8f, 0.6f, 1f);
                sparksMain.startSize = 0.05f;
                sparksMain.startLifetime = 0.2f;
                sparksMain.startSpeed = 0.5f;

                ParticleSystem.EmissionModule sparksEmission = sparksParticles.emission;
                sparksEmission.rateOverTime = 20f;

                ParticleSystemRenderer sparksRenderer = sparksParticles.GetComponent<ParticleSystemRenderer>();
                sparksRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
                riftParticles.Play();
                lightningParticles.Play();
                sparksParticles.Play();
                UnityEngine.Object.Destroy(rift, 1.5f);
            }

            if (!ControllerInputPoller.instance.rightControllerPrimaryButton && hasCastVoidRift)
            {
                hasCastVoidRift = false;
            }
        }

        private static bool hasCastFrostOrb = false;

        public static void CastFrostOrb()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton && !hasCastFrostOrb)
            {
                hasCastFrostOrb = true;

                Vector3 startPosition = GorillaTagger.Instance.rightHandTransform.position;
                Quaternion startRotation = GorillaTagger.Instance.rightHandTransform.rotation;
                Vector3 direction = startRotation * Vector3.forward;

                GameObject frostOrb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                frostOrb.transform.position = startPosition + (direction * 0.5f);
                frostOrb.transform.rotation = startRotation;
                frostOrb.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

                Rigidbody rb = frostOrb.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = direction * 4f;

                Material orbMaterial = new Material(Shader.Find("Particles/Standard Unlit"));
                orbMaterial.color = new Color(0.5f, 0.8f, 1f, 1f);
                frostOrb.GetComponent<Renderer>().material = orbMaterial;

                GameObject mist = new GameObject("FrostMist");
                mist.transform.parent = frostOrb.transform;
                mist.transform.localPosition = Vector3.zero;

                ParticleSystem mistParticles = mist.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule mistMain = mistParticles.main;
                mistMain.startColor = new Color(0.6f, 0.9f, 1f, 0.5f);
                mistMain.startSize = 0.05f;
                mistMain.startLifetime = 1.5f;
                mistMain.startSpeed = 0.3f;
                mistMain.loop = true;
                mistMain.simulationSpace = ParticleSystemSimulationSpace.World;

                ParticleSystem.EmissionModule mistEmission = mistParticles.emission;
                mistEmission.rateOverTime = 20f;

                ParticleSystem.ShapeModule mistShape = mistParticles.shape;
                mistShape.shapeType = ParticleSystemShapeType.Sphere;
                mistShape.radius = 0.2f;

                ParticleSystemRenderer mistRenderer = mistParticles.GetComponent<ParticleSystemRenderer>();
                mistRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));

                mistParticles.Play();

                GameObject snowflakes = new GameObject("FrostSnowflakes");
                snowflakes.transform.parent = frostOrb.transform;
                snowflakes.transform.localPosition = Vector3.zero;

                ParticleSystem snowParticles = snowflakes.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule snowMain = snowParticles.main;
                snowMain.startColor = Color.white;
                snowMain.startSize = 0.05f;
                snowMain.startLifetime = 1.5f;
                snowMain.startSpeed = 0.2f;
                snowMain.loop = true;
                snowMain.simulationSpace = ParticleSystemSimulationSpace.World;

                ParticleSystem.EmissionModule snowEmission = snowParticles.emission;
                snowEmission.rateOverTime = 30f;

                ParticleSystem.ShapeModule snowShape = snowParticles.shape;
                snowShape.shapeType = ParticleSystemShapeType.Sphere;
                snowShape.radius = 0.3f;

                ParticleSystemRenderer snowRenderer = snowParticles.GetComponent<ParticleSystemRenderer>();
                snowRenderer.material = new Material(Shader.Find("Particles/Standard Unlit"));

                snowParticles.Play();

                UnityEngine.Object.Destroy(frostOrb, 5f);
            }

            if (!ControllerInputPoller.instance.rightControllerPrimaryButton && hasCastFrostOrb)
            {
                hasCastFrostOrb = false;
            }
        }



        public static void CreateDomain()
        {
            Vector3 spawnPosition = new Vector3(-63.2589f, 9.4352f, -65.2775f);

            for (int i = 0; i < 30; i++) 
            {
                GameObject lineObject = new GameObject("LineRenderer_" + i);
                LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
                lineRenderer.startColor = new Color(0.5f, 0f, 0f);
                lineRenderer.endColor = new Color(0f, 0f, 0f); 
                lineRenderer.positionCount = 2;
                Vector3 randomPosition = spawnPosition + new Vector3(
                    UnityEngine.Random.Range(-30, 30f),
                    UnityEngine.Random.Range(-30, 30f),
                    UnityEngine.Random.Range(-30, 30f)
                );

                lineRenderer.SetPosition(0, spawnPosition);  
                lineRenderer.SetPosition(1, randomPosition); 

                UnityEngine.Object.Destroy(lineObject, 2f);
            }

            for (int j = 0; j < 20; j++) 
            {
                GameObject obj;
                if (UnityEngine.Random.Range(0, 2) == 0)
                    obj = GameObject.CreatePrimitive(PrimitiveType.Cube); 
                else
                    obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                obj.transform.position = spawnPosition + new Vector3(
                    UnityEngine.Random.Range(-10f, 10f),
                    UnityEngine.Random.Range(-5f, 5f),
                    UnityEngine.Random.Range(-10f, 10f)
                );

                obj.transform.localScale = new Vector3(
                    UnityEngine.Random.Range(0.2f, 0.5f), 
                    UnityEngine.Random.Range(0.2f, 0.5f),
                    UnityEngine.Random.Range(0.2f, 0.5f)
                );

                obj.GetComponent<Renderer>().material.color = new Color(0.3f, 0f, 0f); 
                GameObject.Destroy(obj.GetComponent<Collider>());
                UnityEngine.Object.Destroy(obj, 2f);
            }
        }
    }
}
