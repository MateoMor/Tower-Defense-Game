# Documentación Sr.Tostada and Mr.Pato

## Descripción General

El objetivo del juego es defender una base de oleadas enemigas utilizando torres de defensa. Es necesario colocar torres de manera estratégica y gesitonarlas

## Estructura del Proyecto

### 1. Sistema de Gestión del Juego

- **GameManager**: Controla el estado general del juego (ganado, perdido, en progreso).
- **PlayerStats**: Administra los recursos del jugador (vidas, dinero) y lleva la cuenta de las rondas sobrevividas.
- **SceneFader**: Maneja las transiciones entre escenas con efectos de desvanecimiento.

### 2. Interfaz de Usuario

- **MainMenu**: Pantalla principal con opciones para iniciar el juego o salir.
- **PauseMenu**: Permite pausar el juego, reintentar el nivel o volver al menú principal.
- **LevelSelector**: Interfaz para seleccionar diferentes niveles.
- **Shop**: Muestra las torres disponibles para comprar y colocar.
- **NodeUI**: Interfaz para mejorar o vender torres ya colocadas.
- **MoneyUI/LivesUI**: Muestran los recursos actuales del jugador.

### 3. Sistema de Oleadas de Enemigos

- **WaveSpawner**: Controla la generación de oleadas de enemigos según la configuración especificada.
- **Wave**: Define el tipo de enemigo, la cantidad y la frecuencia de aparición en cada oleada.
- **Enemy**: Comportamiento básico del enemigo, incluyendo movimiento, vida, valor al morir y capacidad de ataque.

### 4. Sistema de Torres

- **Turret**: Comportamiento básico de las torres, incluye detección de enemigos, rotación hacia el objetivo y ataque.
- **Bullet**: Proyectil disparado por las torres que causa daño a los enemigos.
- **TurretBlueprint**: Define las propiedades de cada tipo de torre (costo, mejoras, etc.).

### 5. Sistema de Colocación de Torres

- **BuildManager**: Administra la selección y construcción de torres.
- **Node**: Representa los espacios donde se pueden construir torres, maneja la interacción con el mouse y la lógica de construcción.

### 6. Sistema de Navegación de Enemigos

- **Waypoints**: Define el camino que siguen los enemigos desde el punto de aparición hasta el objetivo.

## Flujo de Juego

1. **Inicio**: El jugador comienza en el menú principal donde puede iniciar el juego o seleccionar un nivel específico.

2. **Preparación**: El jugador recibe una cantidad inicial de dinero para construir torres antes de que comience la primera oleada.

3. **Construcción de Defensa**: El jugador selecciona y coloca torres en los nodos disponibles en el mapa.

4. **Oleadas de Enemigos**: Los enemigos aparecen en el punto de inicio y siguen el camino predefinido hacia el objetivo.

5. **Defensa**: Las torres atacan automáticamente a los enemigos dentro de su rango.

6. **Recursos**: El jugador gana dinero al derrotar enemigos, que puede usar para construir más torres o mejorar las existentes.

7. **Vida del Jugador**: Cada enemigo que llega al final del camino resta vidas al jugador.

8. **Finalización**:
   - **Victoria**: El jugador supera todas las oleadas definidas para el nivel.
   - **Derrota**: El jugador pierde todas sus vidas.

## Características Principales

### Torres

Las torres son la principal herramienta de defensa del jugador:

1. **Torres Estándar**: Disparan proyectiles a los enemigos.
2. **Torres Láser**: Infligen daño continuo y ralentizan a los enemigos.
3. **Sistema de Mejora**: Las torres pueden ser mejoradas para aumentar su rango, daño y velocidad de ataque.
4. **Venta de Torres**: Las torres pueden ser vendidas para recuperar parte de la inversión.

### Enemigos

Los enemigos vienen en diferentes tipos con propiedades distintas:

1. **Características**: Velocidad, salud, valor monetario.
2. **Comportamientos Especiales**: Algunos enemigos pueden tener la capacidad de atacar torres.
3. **Sistema de Vida**: Barra de vida visual que muestra el estado de salud del enemigo.

### Niveles

El juego ofrece múltiples niveles con:

1. **Diseños Únicos**: Diferentes caminos y configuraciones de nodos.
2. **Progresión de Dificultad**: Oleadas más difíciles a medida que avanza el juego.
3. **Sistema de Selección**: Interfaz para elegir entre diferentes niveles disponibles.

## Controles

- **Mouse**: Selección y colocación de torres.
- **Tecla Escape (ESC) o P**: Pausar el juego.
- **Interfaz de usuario**: Botones para comprar, mejorar y vender torres.

## Elementos Técnicos

### Efectos Visuales

- Efectos de partículas para explosiones, impactos de láser y construcción de torres.
- Transiciones suaves entre escenas.
- Indicadores visuales para la interacción del usuario (colores de nodos al pasar el mouse).

## Extensibilidad

El diseño modular del juego permite:

1. **Nuevos Tipos de Torres**: Fácil adición de nuevas torres con comportamientos únicos.
2. **Nuevos Enemigos**: Implementación de enemigos con diferentes características y comportamientos.
3. **Nuevos Niveles**: Creación de mapas adicionales con configuraciones específicas.

