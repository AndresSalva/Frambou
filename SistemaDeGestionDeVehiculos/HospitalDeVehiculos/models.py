from django.db import models

# Create your models here.
# app/models.py
from django.db import models
from django.utils import timezone
from decimal import Decimal


class TimeStampedModel(models.Model):
    creado_en = models.DateTimeField(auto_now_add=True)
    actualizado_en = models.DateTimeField(auto_now=True)

class Usuario(TimeStampedModel):
    primer_nombre = models.CharField(max_length=120)
    segundo_nombre = models.CharField(max_length=120, null=True)
    primer_apellido = models.CharField(max_length=120)
    segundo_apellido = models.CharField(max_length=120, null=True)
    ci = models.CharField(max_length=120)

    def __str__(self):
        return f"{self.nombre} <{self.email}>"

    def login(self):
        pass

    def logout(self):
        pass


class Administrador(Usuario):
    #email = models.EmailField(unique=True)
    #password = models.CharField(max_length=255)  

    def registrar_vehiculo(self, **datos):
        return Vehiculo.objects.create(**datos)

    def registrar_usuario(self, **datos):
        return Usuario.objects.create(**datos)

    def gestionar_repuestos(self):
        return InventarioRepuestos.objects.filter(administrador=self)


class Cliente(Usuario):
    def solicitar_mantenimiento(self, vehiculo, tipo, costo=Decimal("0.00"), fecha=None):
        fecha = fecha or timezone.now()
        return Mantenimiento.objects.create(
            vehiculo=vehiculo, fecha=fecha, tipo=tipo, costo=costo, estado=Mantenimiento.Estado.PROGRAMADO
        )

    def consultar_vehiculos(self):
        return self.vehiculos.all()



class RepositorioVehiculos(models.Manager):
    def guardar_vehiculo(self, vehiculo: "Vehiculo"):
        vehiculo.save()
        return vehiculo

    def leer_vehiculo(self, pk: int):
        return self.get(pk=pk)

    def listar_vehiculos(self):
        return self.all()


class Vehiculo(TimeStampedModel):
    owner = models.ForeignKey(Cliente, related_name="vehiculos", null=True, blank=True, on_delete=models.SET_NULL)

    marca = models.CharField(max_length=80)
    modelo = models.CharField(max_length=80)
    anio = models.PositiveIntegerField(verbose_name="Año")
    kilometraje = models.PositiveIntegerField(default=0)

    objects = RepositorioVehiculos()

    def __str__(self):
        return f"{self.marca} {self.modelo} ({self.anio})"
    
    @classmethod
    def registrar(cls, **datos):
        return cls.objects.create(**datos)

    def actualizar_km(self, nuevo_km: int):
        if nuevo_km < self.kilometraje:
            raise ValueError("El kilometraje no puede disminuir.")
        self.kilometraje = nuevo_km
        self.save(update_fields=["kilometraje", "actualizado_en"])


class VehiculoParticular(Vehiculo):
    tipo_uso = models.CharField(max_length=60)


class VehiculoComercial(Vehiculo):
    capacidad_carga = models.PositiveIntegerField(help_text="Capacidad de carga en kg")

class Repuesto(TimeStampedModel):
    nombre = models.CharField(max_length=120)
    precio = models.DecimalField(max_digits=12, decimal_places=2)
    stock = models.IntegerField(default=0)

    def __str__(self):
        return f"{self.nombre} (${self.precio})"
    
    def actualizar_stock(self, delta: int):
        self.stock = max(0, self.stock + int(delta))
        self.save(update_fields=["stock", "actualizado_en"])


class InventarioRepuestos(TimeStampedModel):
    administrador = models.ForeignKey(Administrador, on_delete=models.CASCADE, related_name="inventarios")
    repuestos = models.ManyToManyField(Repuesto, blank=True, related_name="inventarios")
    def agregar_repuesto(self, repuesto: Repuesto):
        self.repuestos.add(repuesto)

    def eliminar_repuesto(self, repuesto: Repuesto):
        self.repuestos.remove(repuesto)

    def listar_repuestos(self):
        return self.repuestos.all()

class RepositorioMantenimientos(models.Manager):
    def guardar_mantenimiento(self, m: "Mantenimiento"):
        m.save()
        return m

    def leer_mantenimiento(self, pk: int):
        return self.get(pk=pk)

    def listar_mantenimientos(self):
        return self.select_related("vehiculo")


class Mantenimiento(TimeStampedModel):
    class Estado(models.TextChoices):
        PROGRAMADO = "PROGRAMADO", "Programado"
        EJECUTADO = "EJECUTADO", "Ejecutado"

    vehiculo = models.ForeignKey(Vehiculo, on_delete=models.CASCADE, related_name="mantenimientos")
    fecha = models.DateTimeField()
    tipo = models.CharField(max_length=100)
    costo = models.DecimalField(max_digits=12, decimal_places=2, default=Decimal("0.00"))
    estado = models.CharField(max_length=20, choices=Estado.choices, default=Estado.PROGRAMADO)

    objects = RepositorioMantenimientos()

    def __str__(self):
        return f"{self.tipo} • {self.vehiculo} • {self.fecha:%Y-%m-%d}"

    def programar(self, fecha=None):
        if fecha:
            self.fecha = fecha
        self.estado = self.Estado.PROGRAMADO
        self.save(update_fields=["fecha", "estado", "actualizado_en"])

    def ejecutar(self, costo_final: Decimal | None = None, fecha=None):
        if costo_final is not None:
            self.costo = Decimal(costo_final)
        self.fecha = fecha or timezone.now()
        self.estado = self.Estado.EJECUTADO
        self.save(update_fields=["costo", "fecha", "estado", "actualizado_en"])