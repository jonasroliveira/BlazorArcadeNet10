let scene, camera, renderer, wall, dotNetHelper;

window.initThreeJS = (containerId, dotNetReference) => {
  const container = document.getElementById(containerId);
  if (!container) return;

  const bgColor = 0x0a0a12;

  dotNetHelper = dotNetReference;

  scene = new THREE.Scene();

  scene.background = new THREE.Color(bgColor);

  scene.fog = new THREE.Fog(bgColor, 1, 10);

  camera = new THREE.PerspectiveCamera(
    75,
    container.clientWidth / container.clientHeight,
    0.1,
    1000
  );

  renderer = new THREE.WebGLRenderer();

  renderer.setSize(container.clientWidth, container.clientHeight);

  container.appendChild(renderer.domElement);

  const geometry = new THREE.BoxGeometry(2, 2, 0.5);
  const material = new THREE.MeshStandardMaterial({ color: 0xbb0000 });

  wall = new THREE.Mesh(geometry, material);
  wall.position.z = -5;
  scene.add(wall);

  const light = new THREE.PointLight(0xffffff, 1, 100);
  light.position.set(5, 5, 5);
  scene.add(light);

  camera.position.z = 0;

  setInterval(() => {
    window.shootRay();
  }, 500);

  function animate() {
    requestAnimationFrame(animate);
    renderer.render(scene, camera);
  }

  animate();

  window.gameCamera = camera;
};

window.moveCamera = (dist) => {
  if (window.gameCamera) window.gameCamera.translateZ(-dist);
};

window.rotateCamera = (angle) => {
  if (window.gameCamera) window.gameCamera.rotation.y += angle;
};

// raios
window.shootRay = () => {
  if (!scene || !camera || !wall) return;

  const randomX = (Math.random() - 0.5) * 10;
  const startPoint = new THREE.Vector3(randomX, 1.0, -70);
  const endPoint = new THREE.Vector3(randomX, 1.0, 5);

  const points = [startPoint, endPoint];
  const lineGeo = new THREE.BufferGeometry().setFromPoints(points);
  const lineMat = new THREE.LineBasicMaterial({ color: 0xff00ff });
  const rayLine = new THREE.Line(lineGeo, lineMat);

  scene.add(rayLine);

  const raycaster = new THREE.Raycaster();
  const direction = new THREE.Vector3(0, 0, 1);
  raycaster.set(startPoint, direction);

  const intersects = raycaster.intersectObject(wall);

  if (intersects.length > 0) {
    wall.material.color.set(0xffffff);
    setTimeout(() => wall.material.color.set(0xbb0000), 100);

    if (dotNetHelper) {
      dotNetHelper.invokeMethodAsync("AdicionarPontoInstancia");
    }

    wall.position.x += (Math.random() - 0.5) * 0.2;
  }
  setTimeout(() => scene.remove(rayLine), 200);
};
